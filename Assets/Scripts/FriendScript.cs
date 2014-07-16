using UnityEngine;
using System.Collections;

public class FriendScript : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void SayHello(FriendActions fa, string message) {
		fa.Speak (message);
	}

	public void SayGoodbye(FriendActions fa) {
		fa.StopSpeaking ();
	}
}
