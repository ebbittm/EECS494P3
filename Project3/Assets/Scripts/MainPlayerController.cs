using UnityEngine;
using System.Collections;

public enum State { STAND, CROUCH, CRAWL, RUN };


public class MainPlayerController : MonoBehaviour {
    public static MainPlayerController Instance;

    public Vector3 Movement;
    public Vector3 Rotation;
    public GameObject Player;
    public float Gravity = 21f;
    public float TerminalVelocity = 20f;
    public float VerticalVelocity;

    public float RotationSpeed = 10.0f;
    public float WalkSpeed = 5.0f;
    public float CrouchSpeed = 3.0f;
    public float JumpSpeed = 10.0f;
    public float RunSpeed = 10.0f;
    public float MoveSpeed = 5.0f;

    public State CurrentState = State.STAND;


    void Awake () {
        Instance = this;
        Player = GameObject.Find("Player");
    }

    public void UpdatePlayer () {
        HandleMovement();
        HandleRotation();
	}

    void HandleMovement()
    {
        Movement *= MoveSpeed; ;
        Movement = new Vector3(Movement.x, VerticalVelocity, Movement.z);
        HandleGravity();
        Player1Controller.CharacterController.Move(Movement * Time.deltaTime);
    }

    void HandleRotation()
    {
        transform.Rotate(Rotation * RotationSpeed);
    }

    void HandleGravity()
    {
        if(Movement.y > -TerminalVelocity)
        {
            Movement = new Vector3(Movement.x, Movement.y - Gravity * Time.deltaTime, Movement.z);
        }
        if(Player1Controller.CharacterController.isGrounded && Movement.y < - 1)
        {
            Movement = new Vector3(Movement.x, -1, Movement.z);

        }
    }

    public void HandleJump()
    {
        if (Player1Controller.CharacterController.isGrounded && CurrentState == State.STAND)
        {
            VerticalVelocity = JumpSpeed;
        }
    }

    public void HandleToggleCrouch()
    {
        if(Player1Controller.CharacterController.isGrounded)
        {
            if (CurrentState == State.STAND)
            {
                MoveSpeed = CrouchSpeed;
                CurrentState = State.CROUCH;
                transform.localScale = new Vector3(1.5f, 5.0f, 1.5f);
                Player1Controller.Instance.CurrentScale = 2.5f;
            }
            else if (CurrentState == State.CROUCH)
            {
                MoveSpeed = WalkSpeed;
                CurrentState = State.STAND;
                transform.localScale = new Vector3(1.0f, 2.5f, 1.0f);
                Player1Controller.Instance.CurrentScale = 5.0f;
            }
        }
    }

    public void HandleRun()
    {
        if(Player1Controller.CharacterController.isGrounded && CurrentState == State.STAND)
        {
            MoveSpeed = RunSpeed;
        }
    }

    public void HandleWalk()
    {
        if (Player1Controller.CharacterController.isGrounded && CurrentState == State.STAND)
        {
            MoveSpeed = WalkSpeed;
        }
    }
}
