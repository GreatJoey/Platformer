using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DeathScreen : MonoBehaviour
{
    public GameObject ScreenDisplay;
    public Health liferegister;
    public Button RestartButton;
    public Button QuitButton;

    void Start()
    {
        ScreenDisplay.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        //TESTING DEATH SCREEN Because Death Doesnt Work Yet//
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            ScreenDisplay.SetActive(true);
            Time.timeScale = 0f;
        }

        //Actual Death Condition//
        if (liferegister.currentHealth == 0)
        {
            ScreenDisplay.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RestartGame()
    {
        string CurrentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(CurrentScene);
        Time.timeScale = 1f;
    }

    public void Quitgame()
    {
        Application.Quit();
    }
}
