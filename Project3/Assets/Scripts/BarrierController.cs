using UnityEngine;
using System.Collections;

public class BarrierController : MonoBehaviour {

	public GameObject[] lights;
	public BitArray lightsInfo;
	public Puzzle[] puzzles;
	public Material offMat;
	public Material onMat;

	private int light_index = 0;
	private bool isOpen = false;

	// Use this for initialization
	void Start () {
		lightsInfo = new BitArray (lights.Length, false);
		foreach (GameObject light in lights) {
			light.gameObject.GetComponent<Renderer> ().material = offMat;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//check to see if any new puzzles have been solved
		for (int i = 0; i < puzzles.Length; ++i) {
			if (puzzles [i].solved) {
				lights [i].gameObject.GetComponent<Renderer> ().material = onMat;
				lightsInfo [i] = true;
			} else {
				lights [i].gameObject.GetComponent<Renderer> ().material = offMat;
				lightsInfo [i] = false;
			}
		}

		//allow door to be opened if final light is on
		if (lightsInfo [lightsInfo.Length - 1]) {
			isOpen = true;
		}
	}

	void OnTriggerEnter(Collider other){
		if (isOpen) {
			BarrierInterpolator.S.ready = true;
		}
	}

}
