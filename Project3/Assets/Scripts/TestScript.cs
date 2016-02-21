using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 Movement = new Vector3(0, 0.5f, 20);
            Vector3 newPos = Movement - other.gameObject.transform.position;
            Player1Controller.CharacterController.Move(newPos);
        }
    }
}
