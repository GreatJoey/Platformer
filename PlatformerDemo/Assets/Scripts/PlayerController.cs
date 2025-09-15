using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D RigidBody;
    CapsuleCollider2D GroundCollider;

    InputAction MoveRight;
    InputAction MoveLeft;
    InputAction Jump;

    float XVelocity;
    float YVelocity;
    float speed = 300;
    float JumpSpeed = 1000;

    bool OnGround = false;
    bool JumpTriggered = false;

    void Awake()
    {
        MoveRight = InputSystem.actions.FindAction("MoveRight");
        MoveLeft = InputSystem.actions.FindAction("MoveLeft");
        Jump = InputSystem.actions.FindAction("Jump");

        RigidBody = GetComponent<Rigidbody2D>();
        GroundCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        OnGround = GroundCollider.IsTouchingLayers(LayerMask.GetMask("Enviroment"));
        PlayerMovement();
    }

    private void GetInput()
    {
        XVelocity = 0;
        if (MoveRight.IsPressed())
        {
            XVelocity = speed;
        }
        if (MoveLeft.IsPressed())
        {
            XVelocity = -speed;
        }
        if (Jump.triggered && OnGround)
        {
            JumpTriggered = true;
        }
    }

    private void PlayerMovement()
    {
        if (JumpTriggered)
        {
            RigidBody.linearVelocityY = JumpSpeed * Time.fixedDeltaTime;
            JumpTriggered = false;
        }
        RigidBody.linearVelocityX = XVelocity * Time.fixedDeltaTime;

        Vector2 newVelocity = new Vector2(0, YVelocity);
        RigidBody.AddForce(newVelocity);
    }
}
