using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuHud : MonoBehaviour
{
    public GameObject MenuHUD;
    public Button Resume;
    public Button Quit;

    void Start()
    {
        MenuHUD.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (MenuHUD.activeInHierarchy == false)
            {
                MenuHUD.SetActive(true);
                Time.timeScale = 0f;
            }
        }

    }

    public void unpause()
    {
        MenuHUD.SetActive(false);
        Time.timeScale = 1f;
    }

    public void leavegame()
    {
        Application.Quit();
    }
}
