using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject towerBuildPanel;
    public GameObject towerUpgradePanel;
    public GameObject mapGrid;
    public GameObject resourceManager;


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

    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI scrapText;
    [SerializeField] private TextMeshProUGUI scoreText;

    GameObject tile = null;


    void Update()
    {
        if (resourceManager != null)
        {
            var rm = resourceManager.GetComponent<ResourceManagerScript>();
            hpText.text = $"HP: {rm.getHP()}";
            scrapText.text = $"Scrap: {rm.getMoney()}";
            scoreText.text = $"Total Score: {rm.getScore()}";
        }

        
    }


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
        if (resourceManager.GetComponent<ResourceManagerScript>().getMoney()>=50)
        {
            (int x, int y) = this.tile.GetComponent<Tile>().getCoords();
            this.mapGrid.GetComponent<GridManager>().setTurret(1, x,y);
            CloseAllPanels();
        }
        

    }

    private void BuyPrecisionLaser()
    {
         if (resourceManager.GetComponent<ResourceManagerScript>().getMoney()>=50)
        {
            (int x, int y) = this.tile.GetComponent<Tile>().getCoords();
            this.mapGrid.GetComponent<GridManager>().setTurret(2, x,y);
            resourceManager.GetComponent<ResourceManagerScript>().spendMoney(50);
            CloseAllPanels();
        }

    }

     private void BuyEMPTower()
    {
         if (resourceManager.GetComponent<ResourceManagerScript>().getMoney()>=50)
        {
            (int x, int y) = this.tile.GetComponent<Tile>().getCoords();
            this.mapGrid.GetComponent<GridManager>().setTurret(3, x,y);
            resourceManager.GetComponent<ResourceManagerScript>().spendMoney(50);
            CloseAllPanels();
        }

    }

    private void UpgradeTower()
    {
                CloseAllPanels();

    }

}
