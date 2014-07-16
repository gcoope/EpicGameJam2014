using UnityEngine;
using System.Collections;


public class NonPhysicsPlayerTester : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;

	private FriendScript fs;
	private bool _greeting = false;
	private bool playing = true;
	private bool teleporting = false;
	private bool changingclothes = false;
	private bool ducktalked = false;
	private int convIndex = 0;
	private GameObject collidingFriend;
	private GameObject splashy;

	public GameObject deadpig;
	public GameObject soundobj;


	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}

	void Start() {	
		fs = GetComponent<FriendScript> ();
		splashy = GameObject.Find ("SplashSystem");
		splashy.SetActive (false);
		soundobj = GameObject.Find ("SoundObject");
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		if (col.gameObject.tag == "Friend") {
			_greeting = true;
			collidingFriend = col.gameObject;
		}

		if (col.gameObject.name == "Paddle") {
			splashy.SetActive(true);
		}

		if (col.gameObject.name == "ExplodePig") {
			Instantiate(deadpig,transform.position,Quaternion.identity);
			Destroy(gameObject);
			soundobj.GetComponent<SoundScript>().EndingScreen();
		}
	}


	void onTriggerExitEvent( Collider2D col )
	{
		if (col.gameObject.tag == "Friend") {
			fs.SayGoodbye (col.GetComponent<FriendActions> ());
			_greeting = false;
			convIndex = 0;
		}

		if (col.gameObject.name == "Paddle") {
			splashy.SetActive(false);
		}
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
			playing = !playing;
		}

		if (!playing && !teleporting && !_greeting) {		
			Camera.main.transform.GetChild(0).GetComponent<TextMesh>().text = "By George & Hannah";
			Camera.main.transform.GetChild(1).GetComponent<TextMesh>().text = "Paused";
			Camera.main.transform.GetChild(2).GetComponent<TextMesh>().text = "A game for EpicGameJam 2014";
			_animator.Play (Animator.StringToHash ("Greet"));
		}
		else if(playing && !teleporting)
		{
			Camera.main.transform.GetChild(0).GetComponent<TextMesh>().text = "";
			Camera.main.transform.GetChild(1).GetComponent<TextMesh>().text = "";
			Camera.main.transform.GetChild(2).GetComponent<TextMesh>().text = "";

			if(_greeting) {
				if(Input.GetKeyUp(KeyCode.E)) {
					convIndex++;
				}
				handleGreetings();
			}

			// grab our current _velocity to use as a base for all calculations
			_velocity = _controller.velocity;

			if (_controller.isGrounded)
					_velocity.y = 0;

			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
					normalizedHorizontalSpeed = 1;
					if (transform.localScale.x < 0f)
							transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);

					if (_controller.isGrounded)
							_animator.Play (Animator.StringToHash ("Run"));
			} else if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
					normalizedHorizontalSpeed = -1;
					if (transform.localScale.x > 0f)
							transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);

					if (_controller.isGrounded)
							_animator.Play (Animator.StringToHash ("Run"));
			} else {
					normalizedHorizontalSpeed = 0;

					if (_controller.isGrounded) {
							if (_greeting) {
									_animator.Play (Animator.StringToHash ("Greet"));
							} else {
									_animator.Play (Animator.StringToHash ("Idle"));
							}
					}
			}


			// we can only jump whilst grounded
			if (_controller.isGrounded && (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKey (KeyCode.Space) || Input.GetKey (KeyCode.W))) {
					_velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
					_animator.Play (Animator.StringToHash ("Jump"));
			}


			// apply horizontal speed smoothing it
			var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
			_velocity.x = Mathf.Lerp (_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

			// apply gravity before moving
			_velocity.y += gravity * Time.deltaTime;

			_controller.move (_velocity * Time.deltaTime);
		}
	}
	//###########################//
	// Conversation section start//
	//###########################//
	void handleGreetings(){
		if (collidingFriend.name == "Ass") {
			int AssConversationLength = 7;
			if(convIndex < AssConversationLength) 
			{
				switch(convIndex)
				{
				case 0:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Hello!");
					break;
				case 1:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Welcome to blossom land");
					break;
				case 2:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "My name is Andy the Ass");
					break;
				case 3:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "I will be your guide!");
					break;
				case 4:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Let us teleport to my friend...");
					break;
				case 5:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Adam the Ass!");
					break;
				case 6:
					Instantiate(GameObject.Find("Teleport_particles"), transform.position, Quaternion.identity);
					playing = false;
					teleporting = true;
					Invoke ("Teleport", 1.5f);
					convIndex = AssConversationLength;
					break;
				default:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "");
					break;
				}
			}
		}

		if (collidingFriend.name == "Ass2") {
			int AssConversationLength = 7;
			if(convIndex < AssConversationLength) 
			{
				switch(convIndex)
				{
				case 0:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Hello Piggu!");
					break;
				case 1:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "I am Andy's indentical twin!");
					break;
				case 2:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Adam the Ass!");
					break;
				case 3:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "I should have warned you about that...");
					break;
				case 4:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "...ass to ass teleportation...");
					break;
				case 5:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "My apologies! Enjoy your stay here!");
					break;
				case 6:
					fs.SayGoodbye (collidingFriend.GetComponent<FriendActions> ());
					break;
				default:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "");
					break;
				}
			}
		}

		if (collidingFriend.name == "Duck") {
			if(!ducktalked) {
				int AssConversationLength = 7;
				if(convIndex < AssConversationLength) 
				{
					switch(convIndex)
					{
					case 0:
						fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Hi there!");
						break;
					case 1:
						fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "I am Damon the Duck!");
						break;
					case 2:
						fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "I'm the #1 stylist in Cherry Blossom land!");
						break;
					case 3:
						fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "I can't let you enter the city naked!");
						break;
					case 4:
						fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Here, please choose a piece of clothing...");
						break;
					case 5:
						fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "...it's on the house!");
						break;
					case 6:
						playing = false;
						if(!changingclothes) {
							GetComponent<ClothingScript>().EnableButtons();
							changingclothes = true;
						}
						fs.SayGoodbye (collidingFriend.GetComponent<FriendActions> ());
						break;
					default:
						fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "");
						break;
					}
				}
			}
		}

		if (collidingFriend.name == "Turtle") {
			int AssConversationLength = 6;
			if(convIndex < AssConversationLength) 
			{
				switch(convIndex)
				{
				case 0:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Welcome!");
					break;
				case 1:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "I am Timmy the Turtle!");
					break;
				case 2:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "You look very muddy!");
					break;
				case 3:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "This must be your first time in the city!");
					break;
				case 4:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Have a little paddle to clean yourself up!");
					break;
				case 5:
					GameObject.Find("TurtleBarrier").GetComponent<BarrierScript>().Move();
					fs.SayGoodbye (collidingFriend.GetComponent<FriendActions> ());
					break;
				default:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "");
					break;
				}
			}
		}

		if (collidingFriend.name == "Pony") {
			int AssConversationLength = 7;
			if(convIndex < AssConversationLength) 
			{
				switch(convIndex)
				{
				case 0:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Oh hey there friend...");
					break;
				case 1:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "Fancy meeting you here!");
					break;
				case 2:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "You hungry? You look hungry...");
					break;
				case 3:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "You should try this carrot vodka, dude!");
					break;
				case 4:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "It's a rare and splendid delecacy!");
					break;
				case 5:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "It will blow your mind!");
					break;
				case 6:
					GameObject.Find("PonyBarrier").GetComponent<BarrierScript>().Move();
					fs.SayGoodbye (collidingFriend.GetComponent<FriendActions> ());
					break;
				default:
					fs.SayHello (collidingFriend.GetComponent<FriendActions> (), "");
					break;
				}
			}
		}
	}

	void EndingScreen() {
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	void Teleport(){
		if (Application.loadedLevel + 1 < Application.levelCount) {
						Application.LoadLevel (Application.loadedLevel + 1);
		} else {
			Debug.Log("This was the last level");
		}
	}

	public void SetPlaying(bool p){
		playing = p;	
	}

	public void IncreaseConvIndex() {
		convIndex++;
	}

	public void setDuckTalked(bool d){
		ducktalked = d;
	}

}
