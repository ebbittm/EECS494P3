using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public GameObject doorObj;
    public bool isOpen = false;

    public void OpenDoor()
    {
        doorObj.GetComponent<Animation>().Play("Open");
    }

    public void CloseDoor()
    {
        doorObj.GetComponent<Animation>().Play("Close");
    }
}
