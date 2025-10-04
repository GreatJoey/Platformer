using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class WinScreen : MonoBehaviour
{
    public Inventory WinCon;
    public GameObject WinScreenUI;
    public Button Restart;
    public Button Quit;

    void Start()
    {
        WinScreenUI.SetActive(false);
        Time.timeScale = 1f;
    }


    void Update()
    {
        if (WinCon.Coins == 22)
        {
            WinScreenUI.SetActive(true);
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
