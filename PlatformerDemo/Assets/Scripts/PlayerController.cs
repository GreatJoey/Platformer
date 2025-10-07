using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TMPro.TMP_Text speed_display;
    [SerializeField] private SMScript _sound_manager;
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
    float Base_speed = 300;
    float JumpSpeed = 1000;

    bool OnGround = false;
    bool JumpTriggered = false;

    public void DisplaySpeed(float speed) // there is a text mesh pro problem in here
    {
        speed_display.text = speed.ToString();

    }
/* should move this to test script
    public void reset_position()
    {
        RigidBody.position = Vector3.zero;
        RigidBody.veloctiy = Vector3.zero;

        AutoMove();
    }
*/ 
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

        if (_sound_manager == null)
        {
            GameObject sm_obj = GameObject.Find("SoundManger");
            if (sm_obj != null)
            {
                _sound_manager = sm_obj.GetComponent<SMScript>();
            }
        }
    }

    void Update()
    {
        DisplaySpeed(speed);
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
            if (_sound_manager != null)
            {
                _sound_manager.JumpSound();
            }
        }
        RigidBody.linearVelocityX = XVelocity * Time.fixedDeltaTime;

        //Vector2 newVelocity = new Vector2(0, YVelocity);
        /// RigidBody.AddForce(newVelocity);
    }

    // Buff section
    public void ApplyBigBuff(float multiplier, float duration, float increase_duration)
    {
        StartCoroutine(BigbuffRoutine(multiplier, duration, increase_duration));
    }
    private IEnumerator BigbuffRoutine(float multiplier, float duration, float increase_duration)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * multiplier;
        float elapse = 0f;

        while (elapse < increase_duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapse / increase_duration);
            elapse += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(duration);

        transform.localScale = originalScale;
    }

    public void ApplySpeedBuff(float multiplier, float duration)
    {
        StartCoroutine(SpeedBuffRoutine(multiplier, duration));
    }
    private IEnumerator SpeedBuffRoutine(float multiplier, float duration)
    {
        speed *= multiplier;
        yield return new WaitForSeconds(duration);
        speed = Base_speed;
    }
}
