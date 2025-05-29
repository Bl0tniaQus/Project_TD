using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject towerBuildPanel;
    public GameObject towerUpgradePanel;

    public GameObject upgradeTowerBtn;
    public GameObject addTowerBtn;




    private void Awake()
    {
        if (Instance == null) Instance = this;
        CloseAllPanels();
    }

    public void ShowBuildPanel()
    {
        CloseAllPanels();
        towerBuildPanel.SetActive(true);
        SetMainButtonsVisible(false);
    }

    public void ShowUpgradePanel()
    {
        CloseAllPanels();
        towerUpgradePanel.SetActive(true);
        SetMainButtonsVisible(false);
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
