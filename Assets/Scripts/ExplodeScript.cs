using UnityEngine;
using System.Collections;

public class ExplodeScript : MonoBehaviour {

	void Start () {
		rigidbody2D.AddForce (new Vector2 (Random.Range (-200, 200), Random.Range (100, 300)));
	}
}
