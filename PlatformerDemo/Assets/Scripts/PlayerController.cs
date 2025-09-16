using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D RigidBody;
    CapsuleCollider2D GroundCollider;

    InputAction MoveRight;
    InputAction MoveLeft;
    InputAction Jump;

    SpriteRenderer sprite;

    [SerializeField] private Animator _animator;

    AnimationFSM.FSM fsm = new AnimationFSM.FSM();

    private Vector2 lastFacingDir = Vector2.right;
    private Vector2 FacingDir => lastFacingDir;

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
        //Assert.NotNull(RigidBody);

        GroundCollider = GetComponent<CapsuleCollider2D>();
        //Assert.NotNull(GroundCollider);

        _animator = GetComponent<Animator>();
        //Assert.NotNull(_animator);
        fsm.AddState(new AnimationFSM.JumpState("Jumping"));
        fsm.AddState(new AnimationFSM.WalkState("Walking"));
        fsm.AddState(new AnimationFSM.IdleState("Idle"));

        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        OnGround = GroundCollider.IsTouchingLayers(LayerMask.GetMask("Enviroment"));

        fsm.Update();

        var animatorState = _animator.GetCurrentAnimatorStateInfo(0);
        fsm.conditions.isOnGround = OnGround;
        fsm.conditions.movingX = (XVelocity != 0);

        var fsmAnimationName = fsm.currentState.animationName;
        if (!animatorState.IsName(fsmAnimationName))
        {
            _animator.Play(fsmAnimationName);
        }

        if (XVelocity > 0)
        {
            sprite.flipX = false;
        }
        else if (XVelocity < 0)
        {
            sprite.flipX = true;
        }
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

        //Vector2 newVelocity = new Vector2(0, YVelocity);
       /// RigidBody.AddForce(newVelocity);
    }
}
