using UnityEngine;
using UnityEngine.InputSystem;

public class TestMovement : MonoBehaviour
{
    public Rigidbody2D RigidBody;
    public float speed = 3f;
    public float XVelocity = 0;

    void Update()
    {
        Debug.Log("Getting input");
        GetInput();
    }

    void Start()
    {
        float actual_speed = speed;
    }
    void FixedUpdate()
    {
        Debug.Log("Calling automove");
        AutoMove();
    }
    public void AutoMove()
    {
        if (RigidBody == null)
        {
            Debug.Log("Rigid body is null");
        }
        RigidBody.velocity = new Vector2(speed, RigidBody.velocity.x);
    }

    public void GetInput()
    {
        Debug.Log("we are getting the input");
        XVelocity = 0;
        if (Input.GetKeyDown(KeyCode.D))
        {
            XVelocity = speed;
        }
    }
}
