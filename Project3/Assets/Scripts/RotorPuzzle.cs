using UnityEngine;
using System.Collections;

public class RotorPuzzle : MonoBehaviour {

	public static RotorPuzzle S;

	public Rotor[] rotors;
	public int currRotor = 0;
	public bool solved = false;

	private string solution = "75309";

	void Awake(){
		S = this;
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 5; ++i) {
			rotors [i] = new Rotor (0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (solved)
			return;
		GetRotorInput ();
		if (ReadRotors () == solution) {
			solved = true;
		}
	}

	//function that reads the rotors and returns the current combination
	string ReadRotors() {
		string s = "";
		foreach (Rotor r in rotors) {
			s += r.num.ToString ();
		}
		return s;
	}

	void GetRotorInput(){
		//switches between the rotors
		if (Input.GetKeyDown (KeyCode.Space)) {
			currRotor = (currRotor + 1) % rotors.Length; 
		}
		//Increses the rotor value
		else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			rotors [currRotor].num = (rotors [currRotor].num + 1) % 10;
		}
		//Decreases the rotor value
		else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			rotors[currRotor].num = (rotors[currRotor].num - 1) % 10;
			if (rotors [currRotor].num < 0) {
				rotors [currRotor].num = 9;
			}
		}
	}
}
