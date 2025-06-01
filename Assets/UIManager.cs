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

    [SerializeField] private Image towerImage;
    [SerializeField] private Sprite level1EB;
    [SerializeField] private Sprite level1PL;
    [SerializeField] private Sprite level1ET;
    [SerializeField] private Sprite level2EB;
    [SerializeField] private Sprite level2PL;
    [SerializeField] private Sprite level2ET;
    [SerializeField] private Sprite level3EB;
    [SerializeField] private Sprite level3PL;
    [SerializeField] private Sprite level3ET;
    GameObject tile = null;
    private int cost = 0;

    void Update()
    {
        if (resourceManager != null)
        {
            var rm = resourceManager.GetComponent<ResourceManagerScript>();
            hpText.text = $"HP: {rm.getHP()}";
            scrapText.text = $"Scrap: {rm.getMoney()}";
            scoreText.text = $"Total Score: {rm.getScore()}";
        }
        if(Input.GetMouseButtonDown(1)) 
        {
                CloseAllPanels();
                mapGrid.GetComponent<GridManager>().deHighlightField();
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
        this.cost = 50;
    }

    public void ShowUpgradePanel(GameObject obj)
    {
        string turretName = obj.GetComponent<Tile>().getTurret().name;
        CloseAllPanels();
        towerUpgradePanel.SetActive(true);
        this.tile = obj;
        int level = obj.GetComponent<Tile>().getTurret().GetComponent<projectileAim>().getLevel();
        
        if (level == 1) {this.cost = 500;}
        if (level == 2) {this.cost = 5000;}
        if (level == 3) {this.cost = -1000;}
      
        towerName.text = turretName.Remove(turretName.Length - 7);
        towerLevel.text = "Lvl: " + level.ToString();
        if (level==3){
            towerUpgradeCost.text = "Tower fully upgraded";
        }
        else{
            towerUpgradeCost.text = "Upgrade cost: " + (this.cost).ToString();
        }

        Sprite selectedSprite = null;



        if (turretName == "EnergyBlaster(Clone)")
        {
            if (level == 1) selectedSprite = level1EB;
            else if (level == 2) selectedSprite = level2EB;
            else if (level == 3) selectedSprite = level3EB;
        }
        else if (turretName == "PrecisionLaser(Clone)")
        {
            if (level == 1) selectedSprite = level1PL;
            else if (level == 2) selectedSprite = level2PL;
            else if (level == 3) selectedSprite = level3PL;
        }
        else if (turretName == "EMPTower(Clone)")
        {
            if (level == 1) selectedSprite = level1ET;
            else if (level == 2) selectedSprite = level2ET;
            else if (level == 3) selectedSprite = level3ET;
        }

        
            if(selectedSprite != null){
                towerImage.sprite = selectedSprite;
            }
        

    }

    public void CloseAllPanels()
    {
        towerBuildPanel.SetActive(false);
        towerUpgradePanel.SetActive(false);
        this.cost = 0;
    }
  

    private void BuyEnergyBlaster()
    {
        if (resourceManager.GetComponent<ResourceManagerScript>().getMoney()>=50&&this.tile.GetComponent<Tile>().getType()==2)
        {
            (int x, int y) = this.tile.GetComponent<Tile>().getCoords();
            this.mapGrid.GetComponent<GridManager>().setTurret(1, x,y);
            resourceManager.GetComponent<ResourceManagerScript>().spendMoney(50);
            CloseAllPanels();
        }
        

    }

    private void BuyPrecisionLaser()
    {
         if (resourceManager.GetComponent<ResourceManagerScript>().getMoney()>=50&&this.tile.GetComponent<Tile>().getType()==2)
        {
            (int x, int y) = this.tile.GetComponent<Tile>().getCoords();
            this.mapGrid.GetComponent<GridManager>().setTurret(2, x,y);
            resourceManager.GetComponent<ResourceManagerScript>().spendMoney(50);
            CloseAllPanels();
        }

    }

     private void BuyEMPTower()
    {
         if (resourceManager.GetComponent<ResourceManagerScript>().getMoney()>=50&&this.tile.GetComponent<Tile>().getType()==2)
        {
            (int x, int y) = this.tile.GetComponent<Tile>().getCoords();
            this.mapGrid.GetComponent<GridManager>().setTurret(3, x,y);
            resourceManager.GetComponent<ResourceManagerScript>().spendMoney(50);
            CloseAllPanels();
        }

    }

    private void UpgradeTower()
    {
                int level = this.tile.GetComponent<Tile>().getTurret().GetComponent<projectileAim>().getLevel();
                if (this.cost <= this.resourceManager.GetComponent<ResourceManagerScript>().getMoney() && level<3)
                {
                        this.resourceManager.GetComponent<ResourceManagerScript>().spendMoney(this.cost);
                        this.tile.GetComponent<Tile>().getTurret().GetComponent<projectileAim>().upgrade();
                        CloseAllPanels();
                }
                
                

    }

}
