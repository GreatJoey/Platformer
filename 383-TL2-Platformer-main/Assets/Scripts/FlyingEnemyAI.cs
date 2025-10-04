using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FlyingEnemyAI : MonoBehaviour
{
 [Header("Detection")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float detectionRadius = 6f;
    [SerializeField] private Transform eye; // optional origin for raycasts
    [SerializeField] private LayerMask lineOfSightBlockers; // e.g., Ground
    [SerializeField] private bool requireLineOfSight = true;

    [Header("Shooting")]
    [SerializeField] private Transform firePoint; // where bullets spawn
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float fireRate = 1.5f; // shots/second
    [SerializeField] private float windupTime = 0.25f;
    [SerializeField] private bool faceTarget = true;

    [Header("Strafe Movement (active only when player is detected)")]
    [SerializeField] private Transform patrolCenter;     // optional; if null, uses initial position
    [SerializeField] private float strafeAmplitude = 2f; // units left/right from center
    [SerializeField] private float strafeSpeed = 2f;     // how fast it oscillates
    [SerializeField] private float hoverHeightOffset = 0f; // keep constant Y (centerY + offset)

    [Header("Idle Hover (when no player)")]
    [SerializeField] private bool idleHover = true;
    [SerializeField] private float idleBobAmplitude = 0.1f;
    [SerializeField] private float idleBobSpeed = 1f;

    AnimationFSM.FSM fsm = new AnimationFSM.FSM();

    private Transform _player;
    private Coroutine _shootLoop;
    private Rigidbody2D _rb;

    private float _t;                 // time accumulator for sine motion
    private Vector2 _initialPos;      // starting position (fallback center)
    private float _centerX;           // current strafe center X
    private float _centerY;           // base Y for hover/strafe

    private void Awake()
    {

        fsm.AddState(new AnimationFSM.FlyingState("Flying"));

        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
        _rb.gravityScale = 0f;
        _initialPos = transform.position;
        _centerX = patrolCenter ? patrolCenter.position.x : _initialPos.x;
        _centerY = (patrolCenter ? patrolCenter.position.y : _initialPos.y) + hoverHeightOffset;
    }

    private void Update()
    {
        _player = DetectPlayer();
        bool seesPlayer = _player != null && (!requireLineOfSight || HasLineOfSight(_player.position));

        if (seesPlayer && _shootLoop == null)
        {
            _shootLoop = StartCoroutine(ShootLoop());
            // when we first see the player, reset time so motion starts smoothly
            _t = 0f;
        }
        else if (!seesPlayer && _shootLoop != null)
        {
            StopCoroutine(_shootLoop);
            _shootLoop = null;
        }

        if (faceTarget && _player != null)
        {
            Vector3 dir = (_player.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        HandleMovement(seesPlayer);
    }

    private void HandleMovement(bool seesPlayer)
    {
        _t += Time.deltaTime;

        if (seesPlayer)
        {
            // Strafe left/right around center X while keeping Y ~ constant
            float x = _centerX + Mathf.Sin(_t * strafeSpeed) * strafeAmplitude;
            float y = _centerY;
            _rb.MovePosition(new Vector2(x, y));
        }
        else
        {
            // Idle hover/bob near starting center
            if (idleHover)
            {
                float x = _centerX;
                float y = _centerY + Mathf.Sin(_t * idleBobSpeed) * idleBobAmplitude;
                _rb.MovePosition(new Vector2(x, y));
            }
            // else: stand perfectly still
        }
    }

    private Transform DetectPlayer()
    {
        var center = eye ? eye.position : transform.position;
        var hit = Physics2D.OverlapCircle(center, detectionRadius, playerLayer);
        return hit ? hit.transform : null;
    }

    private bool HasLineOfSight(Vector3 targetPos)
    {
        var origin = eye ? eye.position : transform.position;
        var dir = (targetPos - origin).normalized;
        float dist = Vector2.Distance(origin, targetPos);
        var hit = Physics2D.Raycast(origin, dir, dist, lineOfSightBlockers);
        return hit.collider == null;
    }

    private IEnumerator ShootLoop()
    {
        // small delay so it doesn't fire instantly
        yield return new WaitForSeconds(windupTime);
        var delay = 1f / Mathf.Max(0.01f, fireRate);

        while (true)
        {
            if (_player != null && projectilePrefab != null && firePoint != null)
            {
                Vector2 dir = (_player.position - firePoint.position).normalized;
                var proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                proj.Fire(dir);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        var center = eye ? eye.position : transform.position;
        Gizmos.DrawWireSphere(center, detectionRadius);

        // Draw strafe range (editor-only)
        Gizmos.color = Color.cyan;
        float cX = patrolCenter ? patrolCenter.position.x : (Application.isPlaying ? _centerX : transform.position.x);
        float cY = patrolCenter ? patrolCenter.position.y + hoverHeightOffset
                                : (Application.isPlaying ? _centerY : transform.position.y + hoverHeightOffset);
        Gizmos.DrawLine(new Vector3(cX - strafeAmplitude, cY, 0f), new Vector3(cX + strafeAmplitude, cY, 0f));
        Gizmos.DrawSphere(new Vector3(cX, cY, 0f), 0.05f);
    }
}
