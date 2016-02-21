using UnityEngine;
using System.Collections;

public class RotorController : MonoBehaviour {

	public GameObject[] ui_rotors;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (RotorPuzzle.S.solved)
			return;
		
		SetRotors ();
	}

	void SetRotors(){
		foreach (Rotor r in RotorPuzzle.S.rotors) {
			//here set the ui represenation to be the same as the data
			//ui_rotors
		}
	}
}
