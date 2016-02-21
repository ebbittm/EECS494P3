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

    public bool open = false;

    void Update()
    {
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
