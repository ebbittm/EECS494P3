using UnityEngine;
using System.Collections;

public class Player1Controller : MonoBehaviour {
    //This controller is used by Player 1, and controls the character's directional motion

    public static Player1Controller Instance;
    public static CharacterController CharacterController;

    private bool CrouchToggled = false;
    public float CrouchScale = 2.5f;
    public float StandScale = 5.0f;
    public float CurrentScale = 5.0f;

    public float SlideSpeed = 10.0f;
    public bool Sliding = false;
    public Vector3 Forward;
    public float Timer = 0.0f;
    public float TimerMax;
    
    void Awake () {
        Instance = this;
        CharacterController = GetComponent<CharacterController>() as CharacterController;
	}
	
	void Update () {
        GetMovement();
        GetRotation();
        GetAuxiliaryActionInput();

        if (CrouchToggled)
        {
            float distance = transform.localScale.y / 2;
            float scale = transform.localScale.y;
            float yValue = Mathf.Lerp(transform.localScale.y, CurrentScale, 5 * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.x, yValue, transform.localScale.z);
            float changeDistance = distance * (transform.localScale.y - scale);
            transform.position += new Vector3(0, changeDistance, 0);
            print(transform.localScale);
            if((MainPlayerController.Instance.CurrentState == State.STAND && transform.localScale.y == StandScale) ||
                (MainPlayerController.Instance.CurrentState == State.CROUCH && transform.localScale.y == CrouchScale))
            {
                CrouchToggled = false;
            }
        }

        MainPlayerController.Instance.UpdatePlayer();
    }

    void GetMovement()
    {
        MainPlayerController.Instance.VerticalVelocity = MainPlayerController.Instance.Movement.y;
        MainPlayerController.Instance.Movement = Vector3.zero;
        Transform position = MainPlayerController.Instance.Player.transform;

        if(Input.GetKey(KeyCode.W))
        {
            MainPlayerController.Instance.Movement += position.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            MainPlayerController.Instance.Movement -= position.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            MainPlayerController.Instance.Movement -= position.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MainPlayerController.Instance.Movement += position.right;
        }
    }

    void GetRotation()
    {
        MainPlayerController.Instance.Rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
    }

    void GetAuxiliaryActionInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Input.GetButtonDown("Crouch"))
        {
            ToggleCrouch();
        }
        if (Input.GetAxis("Run") > 0)
        {
            print("Running");
            Run();
        }
        else
        {
            Walk();
        }
    }

    void Jump()
    {
        MainPlayerController.Instance.HandleJump();
    }

    void ToggleCrouch()
    {
        CrouchToggled = true;
        MainPlayerController.Instance.HandleToggleCrouch();
    }

    void Run()
    {
        MainPlayerController.Instance.HandleRun();
    }

    void Walk()
    {
        MainPlayerController.Instance.HandleWalk();
    }
}
