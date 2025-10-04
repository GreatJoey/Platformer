using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StressTest : InputTestFixture
{
    GameObject player;
    Rigidbody2D RigidBody;
    Keyboard keyboard;
    TestMovement movement;

    private int test_num = 0;
    private float wall_position = 2.485f; // this is the x coordinate, coordinate is wrong currently


    [UnitySetUp]
    public IEnumerator SetUp()
    {
        Debug.Log("Setting up");
        yield return new WaitForFixedUpdate();
        yield return SceneManager.LoadSceneAsync("SampleScene");
        yield return new WaitForSeconds(5);

        player = new GameObject("player");
        RigidBody = player.AddComponent<Rigidbody2D>();

        keyboard = InputSystem.AddDevice<Keyboard>();

        movement = player.AddComponent<TestMovement>();
        Debug.Log("we added movement");
        movement.RigidBody = RigidBody;
        movement.speed = 8f;

        var camera_object = new GameObject("Camera");
        var camera = camera_object.AddComponent<Camera>();
        camera_object.tag = "MainCamera";
        camera.orthographic = true;
        camera.orthographicSize = 7.5f;
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,-10);
        
        var sprite = player.AddComponent<SpriteRenderer>();

        var collider = player.AddComponent<CapsuleCollider2D>();
        var animator = player.AddComponent<Animator>();

        var display_object = new GameObject("SpeedGraphic");
        var display = display_object.AddComponent<SpeedDisplay>();
        display.movement = movement;

        foreach (var pc in GameObject.FindObjectsOfType<PlayerController>())
        {
            pc.enabled = false;
        }

        yield return null;
    }

[UnityTest]
    public IEnumerator MoveForward()
    {
        int max_frames = 2000;
        while (true)
        {
            Press(keyboard.dKey);
            Debug.Log("Key is being pressed");
            Vector3 start_position = player.transform.position;
            for (int i = 0; i < max_frames; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            Assert.Greater(player.transform.position.x, start_position.x);
            if (player.transform.position.x > wall_position)
            {
                Assert.Fail();
                break;
            }
            test_num++;
        }
        Release(keyboard.dKey);
        Debug.Log($"Failed on test {test_num}");
        yield return null;
    }


    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(player);
        yield return null;
    }
}

