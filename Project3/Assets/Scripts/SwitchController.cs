using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {

    public GameObject Door;

    public GameObject OffLight;
    public GameObject OnLight;

    public Material OffRed;
    public Material OffGreen;
    public Material OnRed;
    public Material OnGreen;

    public GameObject LightPuzzle;

    public bool open = false;

    void Update()
    {
        if (LightPuzzle.GetComponent<LightPuzzle>().solved && !open)
        {
            open = true;
        }
        if(open)
        {
            OffLight.GetComponent<Renderer>().material = OnRed;
            OnLight.GetComponent<Renderer>().material = OnGreen;
        }
        else
        {
            OffLight.GetComponent<Renderer>().material = OffRed;
            OnLight.GetComponent<Renderer>().material = OffGreen;
        }
    }

    public void DoorInteract()
    {
        if (Door.GetComponent<DoorController>().isOpen)
        {
            Door.GetComponent<DoorController>().CloseDoor();
        }    
        else
        {
            Door.GetComponent<DoorController>().OpenDoor();
        }
    }

	public void BarrierInteract(){
		if (!Door.GetComponent<DoorController> ().isOpen) {
			Door.GetComponent<DoorController> ().RemoveBarrier ();
		}
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player1Controller.Instance.CloseToPuzzle = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player1Controller.Instance.CloseToPuzzle = false;
        }
    }
}
