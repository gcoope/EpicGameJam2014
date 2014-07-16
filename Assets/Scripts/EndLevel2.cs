using UnityEngine;
using System.Collections;

public class EndLevel2 : MonoBehaviour {

	private GameObject player;
	private bool changed = false;
	
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	void Update () {
		if (!changed) {
			if (player.transform.position.x > 157) {
				player.transform.position = new Vector3(214.1f, -2, 0);
				changed = true;
			}
		}
	}
}
