using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

    public GameObject upArrow;
    public Canvas canvas;
    public GameObject upArrowSpawn;

	// Use this for initialization
	void Awake () {
       
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void SpawnButtons()
    {
        GameObject arrow;
        //arrow.transform.SetParent(canvas.transform);
        arrow = Instantiate(upArrow) as GameObject;
        arrow.transform.SetParent(canvas.transform);
        arrow.transform.position = upArrowSpawn.transform.position;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("player");
            if(Input.GetKeyDown(KeyCode.E))
            {
                print("e");
                SpawnButtons();
            }
        }
    }
}
