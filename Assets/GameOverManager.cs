using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameOverManager : MonoBehaviour
{
    [SerializeField] public GameObject endGamePanel;
    [SerializeField] private Button backToMenuBtn;
    [SerializeField] private Button restartGameBtn;
    [SerializeField] private TextMeshProUGUI totalScore;
    private bool isGameOver = false;

    void Start()
    {
        endGamePanel.SetActive(false);
    }

    public void TriggerGameOver(long score)
    {
        
    totalScore.text = "End Game\n\nTotal Score: " + score.ToString();
    Debug.Log("xd");
    isGameOver = true;
    endGamePanel.SetActive(true);
    Time.timeScale = 0f; 

        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }
}
