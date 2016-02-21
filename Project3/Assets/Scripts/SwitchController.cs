using UnityEngine;
using System.Collections;

public class SwitchController : MonoBehaviour {

    public GameObject Door;

    void OnMouseDown()
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
}
