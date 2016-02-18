using UnityEngine;
using System.Collections;

public class Player2Controller : MonoBehaviour {
    //This controller is used by Player 2, and controls the character's auxiliary actions

    public static Player2Controller Instance;
    public static CharacterController CharacterController;

    void Awake () {
        Instance = this;
        CharacterController = GetComponent<CharacterController>() as CharacterController;
    }

    void Update () {
        //GetAuxiliaryActionInput();
    }

    
}
