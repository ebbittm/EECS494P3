using UnityEngine;
using System.Collections;

public class RotorPuzzle : MonoBehaviour {

	public static RotorPuzzle S;

	public Rotor[] rotors;
	public bool solved = false;

	public string solution = "12345";

	void Awake(){
		S = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (solved)
			return;
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
}
