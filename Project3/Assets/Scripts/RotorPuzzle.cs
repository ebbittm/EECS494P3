﻿using UnityEngine;
using System.Collections;

public class RotorPuzzle : Puzzle {

	public static RotorPuzzle S;

	public Rotor[] rotors;

	private string solution = "75309";

	void Awake(){
		S = this;
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
