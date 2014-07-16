using UnityEngine;
using System.Collections;

public class FriendActions : MonoBehaviour {

	void Start () {
	
	}
		void Update () {
	
	}

	public void Speak(string message) {
		transform.GetChild (0).GetComponent<TextMesh> ().text = message;
		transform.GetChild (1).GetComponent<TextMesh> ().text = "|";
	}

	public void StopSpeaking(){
		transform.GetChild (0).GetComponent<TextMesh> ().text = "";
		transform.GetChild (1).GetComponent<TextMesh> ().text = "";
	}
}
