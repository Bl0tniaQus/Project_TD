using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    
    [Header("Przyciski Głównego Menu")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    
    [Header("Przyciski Poziomu Trudności")]
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;
    [SerializeField] private Button backButton;
    
    [Header("Przyciski Credits")]
    [SerializeField] private Button backButton2;
    private void Start()
    {
        if(!PlayerPrefs.HasKey("Difficulty"))
        {
            SetDifficulty("medium"); // Ustaw domyślnie "medium"
        }
        else
        {
        string savedDifficulty = PlayerPrefs.GetString("Difficulty");
        SetDifficulty(savedDifficulty); // Podświetli zapisany przycisk
    }
        
        startButton.onClick.AddListener(StartNewGame);
        settingsButton.onClick.AddListener(ShowDifficultyMenu);
        creditsButton.onClick.AddListener(ShowCredits);
        quitButton.onClick.AddListener(QuitGame);
        
        easyButton.onClick.AddListener(() => SetDifficulty("easy"));
        mediumButton.onClick.AddListener(() => SetDifficulty("medium"));
        hardButton.onClick.AddListener(() => SetDifficulty("hard"));
        backButton.onClick.AddListener(ShowMainMenu);
        backButton2.onClick.AddListener(ShowMainMenu);
        
        ShowMainMenu();
    }

    private void StartNewGame()
    {
      
         SceneManager.LoadScene("SampleScene");
    }

    private void ShowDifficultyMenu()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    private void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    private void ShowMainMenu()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    private void SetDifficulty(string difficulty)
    {
    PlayerPrefs.SetString("Difficulty", difficulty);

    
    
    switch(difficulty)
    {
        case "easy":
            ResetButtonColors();
            easyButton.image.color = Color.green;
            
            break;
        case "medium":
            ResetButtonColors();
            mediumButton.image.color = Color.yellow;
            break;
        case "hard":
            ResetButtonColors();
            hardButton.image.color = Color.red;
            break;
    }
        //ShowMainMenu();
        
    }

    private void ResetButtonColors()
{
    easyButton.image.color = Color.white;
    mediumButton.image.color = Color.white;
    hardButton.image.color = Color.white;
}

    private void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}