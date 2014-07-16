using UnityEngine;
using System.Collections;

public class BarrierScript : MonoBehaviour {

	private bool move = false;

	void Update () {
		if (move) 
		{
			if (transform.position.y > -7)
			{
				transform.Translate (new Vector3 (0, -5, 0));
			}
		}
	}

	public void Move() {
		move = true;
	}
}
