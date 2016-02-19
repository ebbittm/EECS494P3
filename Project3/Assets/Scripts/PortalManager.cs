using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalManager : MonoBehaviour {

	public static PortalManager P;
	public List<Portal> portalList = new List<Portal>();
	Dictionary<int, int> portalMap = new Dictionary<int, int>();

	enum PortalMovement {
		pairs,
		cyclic,
		random
	}
	PortalMovement portalMovement = PortalMovement.pairs;

	void Awake () {
		P = this;
	}

	void Start () {
		if (portalMovement == PortalMovement.pairs) {
			for (int i=0; i< portalList.Count; i += 2) {
				if (i < portalList.Count - 1) {
					portalMap [i] = i + 1;
				} else {
					portalMap [i] = i;
				}
			}
		} else if (portalMovement == PortalMovement.cyclic) {
			for (int i=0; i< portalList.Count; i++) {
				if (i == portalList.Count - 1) {
					portalMap [i] = 0;
				} else {
					portalMap [i] = i + 1;
				}
			}

		}
	}

	public Vector3 portalMove(int portalNum) {
		int nextPortal = portalNum;

		if (portalMovement == PortalMovement.random) {
			while (nextPortal == portalNum) {
				nextPortal = (Random.Range (0, portalList.Count));
			}
		} else {
			nextPortal = portalMap [portalNum];
		}

		return portalList [nextPortal].transform.position;

	}
}
