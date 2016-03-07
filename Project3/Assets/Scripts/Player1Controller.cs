﻿using UnityEngine;
using System.Collections;

public class Player1Controller : MonoBehaviour {
    public static Player1Controller Instance;
    public static CharacterController CharacterController;

    public Camera PlayerView;
    private MouseLook Mouselook = new MouseLook();

    private bool CrouchToggled = false;
    public float CrouchScale = 2.5f;
    public float StandScale = 5.0f;
    public float CurrentScale = 5.0f;

    public float SlideSpeed = 10.0f;
    public bool Sliding = false;
    public Vector3 Forward;
    public float Timer = 0.0f;
    public float TimerMax = 10.0f;

    public bool CloseToPuzzle;
    public bool ArrowPuzzle;

    public bool Dead;

    public GameObject Compass;
    
    void Awake () {
        Instance = this;
        CharacterController = GetComponent<CharacterController>() as CharacterController;
        PlayerView = Camera.main;
        Mouselook.Init(transform, PlayerView.transform);
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
            if((MainPlayerController.Instance.CurrentState == State.STAND && transform.localScale.y == StandScale) ||
                (MainPlayerController.Instance.CurrentState == State.CROUCH && transform.localScale.y == CrouchScale))
            {
                CrouchToggled = false;
            }
        }
        

        if(MainPlayerController.Instance.CurrentState == State.SLIDE)
        {
            Timer += Time.deltaTime;
            if(Timer > TimerMax)
            {
                MainPlayerController.Instance.HandleToggleCrouch();
                Timer = 0.0f;
            }
        }
        

        MainPlayerController.Instance.UpdatePlayer();
        PlayerDead();
    }

    void GetMovement()
    {
        MainPlayerController.Instance.VerticalVelocity = MainPlayerController.Instance.Movement.y;
        MainPlayerController.Instance.Movement = Vector3.zero;
        Transform position = MainPlayerController.Instance.Player.transform;

        if (MainPlayerController.Instance.CurrentState != State.SLIDE)
        {
            if (Input.GetKey(KeyCode.W))
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
        else
        {
            MainPlayerController.Instance.Movement += position.forward;
        }
    }

    void GetRotation()
    {
        Mouselook.LookRotation(transform, PlayerView.transform);
        //MainPlayerController.Instance.Rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
    }

    void GetAuxiliaryActionInput()
    {
        if (!ArrowPuzzle)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            if (Input.GetButtonDown("Crouch"))
            {
                ToggleCrouch();
            }
            if (Input.GetButtonDown("Interact"))
            {
                Interaction();
            }
            /*
            if (Input.GetKeyDown (KeyCode.E)) {
                Interaction ();
            }
            */
            if (Input.GetKeyDown(KeyCode.C))
            {
                ToggleCompass();
            }
            if (Input.GetAxis("Run") > 0)
            {
                Run();
            }
            else
            {
                Walk();
            }
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

	//Need the duplication or refactoring because the Switch is no longer CloseToPuzzle
    void Interaction()
    {
		RaycastHit hitInfo;
		Camera view = GetComponentInChildren<Camera>();
		Vector3 position = MainPlayerController.Instance.Player.transform.position;
		Vector3 forward = view.transform.forward;
		if(Physics.Raycast(position, forward, out hitInfo)){
			if (hitInfo.collider.gameObject.tag == "Switch" && AllPuzzlesSolved())
			{
				//hitInfo.collider.gameObject.GetComponent<SwitchController>().DoorInteract();
				hitInfo.collider.gameObject.GetComponent<SwitchController>().BarrierInteract();
			}
		}

        //add the CloseToPuzzle back in after presentation
        if(true)
        {
            
            //Camera view = GetComponentInChildren<Camera>();
            //Vector3 position = MainPlayerController.Instance.Player.transform.position;
            //Vector3 forward = view.transform.forward;
            print(forward);
            if (Physics.Raycast(view.transform.position, forward, out hitInfo))
            {
                print(hitInfo.collider.gameObject);
                if (hitInfo.collider.gameObject.tag == "Lever")
                {
                    hitInfo.collider.gameObject.GetComponent<LeverController>().ToggleSwitch();
                }
                
            }
        }
    }

	bool AllPuzzlesSolved(){
		return LightPuzzle.S.solved && RotorPuzzle.S.solved && CanvasPuzzleController.Instance.solved;
	}

    void ToggleCompass()
    {
        if(Compass.activeSelf)
        {
            Compass.SetActive(false);
        }
        else {
            Compass.SetActive(true);
        }
    }


    void PlayerDead()
    {
        if (OxygenController.Instance.CurrentOxygen <= 0)
        {
            //player is dead
            Dead = true;
            Compass.SetActive(false);

        }
    }

}
