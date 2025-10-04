using NUnit.Framework;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Changes by Connor Wolfe (sound)
    [SerializeField] private SMScript sound_manager;
    // -------------------------------

    [SerializeField] private GameObject DeathMenu;

    Rigidbody2D RigidBody;
    CapsuleCollider2D GroundCollider;

    InputAction MoveRight;
    InputAction MoveLeft;
    InputAction Jump;
    SpriteRenderer sprite;

    [SerializeField] private Animator _animator;

    AnimationFSM.FSM fsm = new AnimationFSM.FSM();

    float XVelocity = 0;
    float YVelocity = 0;
    float BaseSpeed = 200;
    float Speed = 200;
    float JumpSpeed = 1000;

    public float actual_speed = 0f;

    bool OnGround = false;

    bool JumpTriggered = false;


    private Vector2 lastFacingDir = Vector2.right;
    public Vector2 FacingDir => lastFacingDir;

    void Awake()
    {
        MoveRight = InputSystem.actions.FindAction("MoveRight");
        MoveLeft = InputSystem.actions.FindAction("MoveLeft");
        Jump = InputSystem.actions.FindAction("Jump");

        RigidBody = GetComponent<Rigidbody2D>();
        Assert.NotNull(RigidBody);

        GroundCollider = GetComponent<CapsuleCollider2D>();
        Assert.NotNull(GroundCollider);

        _animator = GetComponent<Animator>();
        Assert.NotNull(_animator);
        fsm.AddState(new AnimationFSM.IdleState("Idle"));
        fsm.AddState(new AnimationFSM.JumpState("Jump"));
        fsm.AddState(new AnimationFSM.WalkState("Walk"));

        sprite = GetComponent<SpriteRenderer>();

        // Changes by Connor Wolfe (sound)
        if (sound_manager == null) // get the sound manager if not set in Unity
        {
            GameObject sm_obj = GameObject.Find("SoundManager");
            if (sm_obj != null)
                sound_manager = sm_obj.GetComponent<SMScript>();
        }
        // -------------------------------

    }


    // Update is called once per frame
    void Update()
    {
        GetInput();
        // Moved "OnGround collision to "update" so that it updates every frame for animation purposes.
        OnGround = GroundCollider.IsTouchingLayers(LayerMask.GetMask("Environment"));

        // _animator.SetBool("isWalking", XVelocity != 0 && OnGround);
        // _animator.SetBool("isJumping", !OnGround);

        fsm.Update();

        if (_animator != null)
        {
            var animatorState = _animator.GetCurrentAnimatorStateInfo(0);

            // var sensitivity = 0.1f;
            fsm.conditions.isOnGround = OnGround;
            fsm.conditions.movingX = (XVelocity != 0); // (XVelocity > sensitivity ? 1 : 0) + (XVelocity < -sensitivity ? -1 : 0);

            var fsmAnimationName = fsm.currentState.animationName;
            if (!animatorState.IsName(fsmAnimationName))
            {
                _animator.Play(fsmAnimationName);
            }
        }

        if (XVelocity > 0)
            {
                sprite.flipX = false;
                lastFacingDir = Vector2.right;
            }
            else if (XVelocity < 0)
            {
                sprite.flipX = true;
                lastFacingDir = Vector2.left;
            }
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    private void GetInput()
    {
        XVelocity = 0;
        if (MoveRight.IsPressed())
        {
            XVelocity = Speed;
        }
        if (MoveLeft.IsPressed())
        {
            XVelocity = -Speed;
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

            // Changes by Connor Wolfe (sound)
            sound_manager.JumpSound();
            // -------------------------------
        }
        RigidBody.linearVelocityX = XVelocity * Time.fixedDeltaTime;

        Vector2 newVelocity = new Vector2(0, YVelocity);

        RigidBody.AddForce(newVelocity);


        // Restart if player falls of platforms
        string CurrentScene = SceneManager.GetActiveScene().name;
        if (RigidBody.position.y < -10)
        {
            DeathMenu.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    //Buff section
    public void ApplySpeedBuff(float multiplier, float duration)
    {
        StartCoroutine(SpeedBuffRoutine(multiplier, duration));
    }

    private IEnumerator SpeedBuffRoutine(float multiplier, float duration)
    {
        Speed *= multiplier;
        yield return new WaitForSeconds(duration);
        Speed = BaseSpeed;
    }

    public void ApplyBigBuff(float multiplier, float duration, float increaseduration)
    {

        StartCoroutine(BigBuffRoutine(multiplier, duration, increaseduration));
    }

    private IEnumerator BigBuffRoutine(float multiplier, float duration, float increaseduration)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * multiplier;

        float elapse = 0f;

        while (elapse < increaseduration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapse / increaseduration);
            elapse += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(duration);

        transform.localScale = originalScale;
    }
    // test functions
    public void AddSpeed(float speed)
    {
        RigidBody.linearVelocity = new Vector2(speed, RigidBody.linearVelocity.y);
        actual_speed = Mathf.Abs(RigidBody.linearVelocity.x);
    }
    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }
}
