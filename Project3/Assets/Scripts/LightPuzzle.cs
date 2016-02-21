using UnityEngine;
using System.Collections;

//Example Puzzle: 
//	Lights need to be 10011
//	Start at 11111
//First button toggles 1&3
//Second button toggles 2&4
//Last button toggles 1&2

//flip 2&4
//10101

//flip 1&3 
//00001

//flip 1&2
//11001

//flip 2&4
//10011

public class LightPuzzle : MonoBehaviour {

	public static LightPuzzle S;
	public BitArray toggled_lights;
	public BitArray levers;

    public GameObject[] Levers;

	void Awake(){
		S = this;
	}

	// Use this for initialization
	void Start () {
		toggled_lights = new BitArray (5, true);
		levers = new BitArray (3, false);
	}
	
	// Update is called once per frame
	//Listen for input from P2s controller to toggle the lights above the door
	void Update () {
		//GetToggleInput ();
	}

	/*void GetToggleInput(){
		if (Input.GetKeyDown (KeyCode.U)) {
			HandleInput ('u');
		} else if (Input.GetKeyDown(KeyCode.I)){
			HandleInput('i');
		} else if (Input.GetKeyDown(KeyCode.O)){ //The letter
			HandleInput('o');
		}
	}
    */

    //This is the function that implements the logic of flipping lights
    public void HandleInput(string Switch) {
        switch (Switch) {
            case "Lever0":
                FlipBits(new int[1] { 0 }, levers);
                FlipBits(new int[2] { 0, 2 }, toggled_lights);
                break;
            case "Lever1":
                FlipBits(new int[1] { 1 }, levers);
                FlipBits(new int[2] { 1, 3 }, toggled_lights);
                break;
            case "Lever2":
                FlipBits(new int[1] { 2 }, levers);
                FlipBits(new int[2] { 0, 1 }, toggled_lights);
                break;
            default:
                break;
        }
	}

	//function used to flip the bits in the array
	void FlipBits(int[] positions, BitArray ba){
		foreach (int index in positions) {
			if (ba [index]) {
				ba [index] = false;
			} else {
				ba [index] = true;
			}
		}
	}
}
