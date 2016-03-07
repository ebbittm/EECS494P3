using UnityEngine;
using System.Collections;

public class LeverIntroPuzzle : Puzzle {

	public static LeverIntroPuzzle S;
	public GameObject lever;
	private bool leverState = false;

	void Start(){
		S = this;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Z)) {
			HandleInput ();
		}
	}

	public void HandleInput(){
		if (leverState) {
			solved = false;
		} else {
			solved = true;
		}
		leverState = !leverState;
		lever.gameObject.transform.Rotate (0, 0, 90);
	}
}
