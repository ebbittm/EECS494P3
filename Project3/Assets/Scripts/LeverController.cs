using UnityEngine;
using System.Collections;

public class LeverController : MonoBehaviour
{

    public GameObject LightPuzzle;
    private LightPuzzle LP;

    void Awake()
    {
        LP = LightPuzzle.GetComponent<LightPuzzle>();
    }

    public void ToggleSwitch()
    {
        string name = gameObject.name;
        LP.HandleInput(name);
    }
}
