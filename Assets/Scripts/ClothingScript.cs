using UnityEngine;
using System.Collections;

public class ClothingScript : MonoBehaviour {

	public GameObject clothingButton1;
	public GameObject clothingButton2;
	public GameObject clothingButton3;
	
	public GameObject clothingItem1;
	public GameObject clothingItem2;
	public GameObject clothingItem3;

	void Start () {
		HideButtons ();
		HideClothes ();
	}

	void OnMouseUp() {

	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				if(hit.collider.name == "clothes1") {
					clothingItem1.SetActive(true);
					GetComponent<NonPhysicsPlayerTester>().SetPlaying(true);
					GetComponent<NonPhysicsPlayerTester>().IncreaseConvIndex();
					GetComponent<NonPhysicsPlayerTester>().setDuckTalked(true);
					GameObject.Find ("DuckBarrier").GetComponent<BarrierScript> ().Move ();
					HideButtons();
				}	
				if(hit.collider.name == "clothes2") {
					clothingItem2.SetActive(true);
					GetComponent<NonPhysicsPlayerTester>().SetPlaying(true);
					GetComponent<NonPhysicsPlayerTester>().IncreaseConvIndex();
					GetComponent<NonPhysicsPlayerTester>().setDuckTalked(true);
					GameObject.Find ("DuckBarrier").GetComponent<BarrierScript> ().Move ();
					HideButtons();
				}	
				if(hit.collider.name == "clothes3") {
					clothingItem3.SetActive(true);
					GetComponent<NonPhysicsPlayerTester>().SetPlaying(true);
					GetComponent<NonPhysicsPlayerTester>().IncreaseConvIndex();
					GetComponent<NonPhysicsPlayerTester>().setDuckTalked(true);
					GameObject.Find ("DuckBarrier").GetComponent<BarrierScript> ().Move ();
					HideButtons();
				}
			}
		}
	}

	public void EnableButtons() {
		clothingButton1.SetActive (true);
		clothingButton2.SetActive (true);
		clothingButton3.SetActive (true);
	}

	void HideButtons() {
		clothingButton1.SetActive (false);
		clothingButton2.SetActive (false);
		clothingButton3.SetActive (false);
	}

	void HideClothes() {
		clothingItem1.SetActive (false);
		clothingItem2.SetActive (false);
		clothingItem3.SetActive (false);
	}
}
