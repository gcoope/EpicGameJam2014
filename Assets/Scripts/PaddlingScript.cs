using UnityEngine;
using System.Collections;

public class PaddlingScript : MonoBehaviour {


	private GameObject player;
	private bool changed = false;

	void Start () {
		player = GameObject.Find ("Player");
	}
	
	void Update () {
		if (!changed) {
			if (player.transform.position.x > 85) {
				GameObject.Find ("PigguDirt").SetActive (false);
				changed = true;
			}
		}
	}
}
