using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndingScreen() {
		Invoke ("EndingScreen1", 3f);
	}	
	public void EndingScreen1() {
		Application.LoadLevel (2);
	}
}
