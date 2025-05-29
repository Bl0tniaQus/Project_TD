using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject towerBuildPanel;
    public GameObject towerUpgradePanel;

    public GameObject upgradeTowerBtn;
    public GameObject addTowerBtn;

    GameObject tile = null;





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
