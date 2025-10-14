using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StressTest : InputTestFixture
{
    GameObject player;
    Rigidbody2D RigidBody;
//    Keyboard keyboard;
    TestMovement movement;

    public float fail_position = 6.0f;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return new WaitForFixedUpdate();
        yield return SceneManager.LoadSceneAsync("SampleScene");
        yield return new WaitForSeconds(5);

        player = GameObject.Find("Player");
        RigidBody = player.AddComponent<Rigidbody2D>();

//        keyboard = InputSystem.AddDevice<Keyboard>();

        movement = player.AddComponent<TestMovement>();
        movement.RigidBody = RigidBody;

        var camera_object = new GameObject("Camera");
        var camera = camera_object.AddComponent<Camera>();
        camera.orthographic = true;
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
/*
        var sprite = player.AddComponent<SpriteRenderer>();

        var collider = player.AddComponent<CapsuleCollider2D>();
        var animator = player.AddComponent<Animator>();
*/
        yield return null;
    }

    [UnityTest]
    public IEnumerator MoveForward()
    {
        yield return new WaitForSeconds(5);
        int max_frames = 20000;
        int multiplier = 2;
        while (true)
        {
//            Press(keyboard.dKey);
            Vector3 start_position = player.transform.position;
            for (int i = 0; i < max_frames; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            if (movement.hit_wall)
            {
                movement.speed = movement.speed * multiplier;
                Debug.Log("Doubling the speed");
                movement.hit_wall = false;
            }

            Assert.Greater(player.transform.position.x, start_position.x);
            if (player.transform.position.x > fail_position)
            {
                Assert.Fail();
                break;
            }
        }
 //       Release(keyboard.dKey);
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(player);
        yield return null;
    }
}
