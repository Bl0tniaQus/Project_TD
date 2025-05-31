using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Button quitGame;
    [SerializeField] private string mainMenuSceneName = "MainMenu"; 
    [SerializeField] private GameObject pausePanel;
    void Start()
    {
        if(quitGame != null)
        {
            quitGame.onClick.AddListener(OpenPausePanel);        }
        
        if(pausePanel != null)
            pausePanel.SetActive(false);
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

     void OpenPausePanel()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f; // zatrzymaj grę
        }
        else
        {
            Debug.LogError("Nie przypisano panelu pauzy w inspektorze!");
        }
    }
}