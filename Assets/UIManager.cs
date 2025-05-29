using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject towerBuildPanel;
    public GameObject towerUpgradePanel;

    public GameObject upgradeTowerBtn;
    public GameObject addTowerBtn;

    GameObject tile = null;


    void Start()
    {
        ResizePanel(towerBuildPanel);
        ResizePanel(towerUpgradePanel);
    }
    private void ResizePanel(GameObject panel)
    {
        RectTransform rt = panel.GetComponent<RectTransform>();
        float width = Screen.width * 0.25f;
        float height = rt.sizeDelta.y; // zachowujemy obecną wysokość
    
        rt.anchorMin = new Vector2(0, 0.5f); // lewa środkowa kotwica
        rt.anchorMax = new Vector2(0, 0.5f);
        rt.pivot = new Vector2(0, 0.5f);     // obrót z lewej
        rt.sizeDelta = new Vector2(width, height);
        rt.anchoredPosition = new Vector2(0, 0); // przyklejony do lewej krawędzi
    }


    private void Awake()
    {
        if (Instance == null) Instance = this;
        CloseAllPanels();
    }

    public void ShowBuildPanel(GameObject obj)
    {
        CloseAllPanels();
        towerBuildPanel.SetActive(true);
        SetMainButtonsVisible(false);
        this.tile = obj;
    }

    public void ShowUpgradePanel(GameObject obj)
    {
        CloseAllPanels();
        towerUpgradePanel.SetActive(true);
        SetMainButtonsVisible(false);
        this.tile = obj;
    }

    public void CloseAllPanels()
    {
        towerBuildPanel.SetActive(false);
        towerUpgradePanel.SetActive(false);
        SetMainButtonsVisible(true);
    }
    private void SetMainButtonsVisible(bool visible)
    {
        addTowerBtn.SetActive(visible);
        upgradeTowerBtn.SetActive(visible);
    }
}