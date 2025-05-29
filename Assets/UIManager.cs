using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject towerBuildPanel;
    public GameObject towerUpgradePanel;



    [Header("Build Panel buttons")]
    [SerializeField] private Button buyEnergyBlasterBtn;
    [SerializeField] private Button buyPrecisionLaserBtn;
    [SerializeField] private Button buyEMPTowerBtn;
    [SerializeField] private Button closePopupBtn;

    [Header("Upgrade Panel buttons")]
    [SerializeField] private Button upgradeTowerBtn;
    [SerializeField] private Button closePopup2Btn;
    [SerializeField] private TextMeshProUGUI towerUpgradeCost;
    [SerializeField] private TextMeshProUGUI towerLevel;
    [SerializeField] private TextMeshProUGUI towerName;



    GameObject tile = null;





    private void Awake()
    {
        if (Instance == null) Instance = this;
        CloseAllPanels();
        SetupButtonListeners();
    }

     private void SetupButtonListeners()
    {
        // Build Panel
        buyEnergyBlasterBtn.onClick.AddListener(BuyEnergyBlaster);
        buyPrecisionLaserBtn.onClick.AddListener(BuyPrecisionLaser);
        buyEMPTowerBtn.onClick.AddListener(BuyEMPTower);
        closePopupBtn.onClick.AddListener(CloseAllPanels);

        // Upgrade Panel
        upgradeTowerBtn.onClick.AddListener(UpgradeTower);
        closePopup2Btn.onClick.AddListener(CloseAllPanels);
    }

    public void ShowBuildPanel(GameObject obj)
    {
        CloseAllPanels();
        towerBuildPanel.SetActive(true);
        this.tile = obj;
    }

    public void ShowUpgradePanel(GameObject obj)
    {
        CloseAllPanels();
        towerUpgradePanel.SetActive(true);
        this.tile = obj;
    }

    public void CloseAllPanels()
    {
        towerBuildPanel.SetActive(false);
        towerUpgradePanel.SetActive(false);
    }
  

    private void BuyEnergyBlaster()
    {
        CloseAllPanels();

    }

    private void BuyPrecisionLaser()
    {
        CloseAllPanels();

    }

     private void BuyEMPTower()
    {
        CloseAllPanels();

    }

    private void UpgradeTower()
    {
                CloseAllPanels();

    }

}
