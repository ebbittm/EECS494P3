using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public int portalID;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	// Move to the player
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Portal") {
			Portal portal = other.gameObject;
			this.transform.position = PortalManager.P.portalMove(portal.GetComponent()<Portal>.portalID);
		}
	}
	*/
}
