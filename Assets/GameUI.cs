using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Button quitGame;
    [SerializeField] private string mainMenuSceneName = "MainMenu"; 

    void Start()
    {
        if(quitGame != null)
        {
            quitGame.onClick.AddListener(ReturnToMainMenu);
        }
        else
        {
            Debug.LogError("Nie przypisano przycisku w inspektorze!");
        }
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}