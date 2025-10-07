using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class TestMovement : MonoBehaviour
{
    GameObject player;
    public TMPro.TMP_Text speed_display;
    public Rigidbody2D RigidBody;
    public float speed = 1f;
    public float XVelocity = 0;
    public bool hit_wall = false;

    public void DisplaySpeed(float speed) // there is a text mesh pro problem in here
    {
        speed_display.text = speed.ToString();

    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        DisplaySpeed(speed);
        if (hit_wall)
        {
            RigidBody.position = Vector2.zero;
            RigidBody.velocity = Vector2.zero;
            hit_wall = false;
            speed = speed * 2;
        }
        Debug.Log("Fixed update");
        AutoMove();
    }
    public void AutoMove()
    {
        RigidBody.velocity = new Vector2(speed, RigidBody.velocity.y);
    }
    public void GetInput()
    {
        XVelocity = 0;
        if (Keyboard.current.dKey.isPressed)
        {
            XVelocity = speed;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Collided");
            hit_wall = true;
        }
    }

}
