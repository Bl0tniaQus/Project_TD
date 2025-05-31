using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject grid;
    public UIManager uiManager;
    
    GameObject resourceManager;
    GameObject turret;
    private short type; //0 - empty, 1 - core, 2 - floor, 3 - road, 4 - spawner, 5 - turrets
    private char direction;
    private int x, y;
    private float cooldown = 0f;
    int animation;
    float[] probabilities = {0f, 0f, 0f, 0f ,0f, 0f};
    // Start is called before the first frame update
    void Start()
    {
      if (uiManager == null)
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    }
    void Awake()
    {
        setType(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        if (!uiManager.towerBuildPanel.activeSelf && !uiManager.towerUpgradePanel.activeSelf)
        {
            grid.GetComponent<GridManager>().highlightField(this.gameObject);
        }
    
    }
    void FixedUpdate()
    {
        
        if (this.cooldown>0f) {this.cooldown-=Time.deltaTime;}
        else 
        {
            if (type==4) {spawn();}
        }

    }
    public void setType(short type)
    {
        this.type = type;
        if (type==0) {
            
            //this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
            
            }
        else if (type==1) {
            
            this.GetComponent<Animator>().SetInteger("Type", 10);
            }
        else if (type==2) {
            
            //this.GetComponent<SpriteRenderer>().color = new Color(0,255,0);
            
            }
        else if (type==3) {
            this.animation = 30;    
            }
        else if (type==4) {
            
            //this.GetComponent<SpriteRenderer>().color = new Color(180,140,128);
            
            }
        else if (type==5) {
            
            
           // this.GetComponent<SpriteRenderer>().color = new Color(0,0,128);
            
            
            }


        


    }
    public void setDirection(char direction)
    {
        this.direction = direction;
    }
    public void setCoords(int x,int y) {this.x = x; this.y = y;}
    public (int, int) getCoords() {return (x,y);}
    public char getDirection() {return this.direction;}
    public short getType() {return this.type;}
    public void setCooldown(float cd) {this.cooldown = cd;}
    public void setGrid(GameObject g) {this.grid = g;}
    private void spawn()
    {
        setProbabilities();
        int maxDim = grid.GetComponent<GridManager>().maxDim;
        int x_spawn=0, y_spawn=0;
        while(true)
        {
            char dir = grid.GetComponent<GridManager>().getRandomDirection();
            if (dir=='r') {x_spawn = this.x+1; y_spawn = this.y;}
            else if (dir=='l') {x_spawn = this.x-1; y_spawn = this.y;}
            else if (dir=='u') {x_spawn = this.x; y_spawn = this.y+1;}
            else if (dir=='d') {x_spawn = this.x; y_spawn = this.y-1;}

            if ((x_spawn) < 0 || (x_spawn > maxDim-1) || (y_spawn < 0) || (y_spawn > maxDim-1))
            {
                continue;
            }
            else
            {
                Tile t = this.grid.GetComponent<GridManager>().getTile(x_spawn, y_spawn);
                int type = t.GetComponent<Tile>().getType();
                if (type!=3) {continue;}
                bool spawned = false;
                GameObject en = this.grid.GetComponent<GridManager>().smallEnemy1;
                int r;
                while (!spawned)
                    {
                            r = Random.Range(0,101);
                            if (r<=this.probabilities[5])
                            {
                                spawned = true;
                                en = this.grid.GetComponent<GridManager>().largeEnemy2;
                                break;
                            }
                            r = Random.Range(0,101);
                            if (r<=this.probabilities[4])
                            {
                                spawned = true;
                                en = this.grid.GetComponent<GridManager>().largeEnemy1;
                                break;
                            }
                            r = Random.Range(0,101);
                            if (r<=this.probabilities[3])
                            {
                                spawned = true;
                                en = this.grid.GetComponent<GridManager>().mediumEnemy2;
                                break;
                            }
                            r = Random.Range(0,101);
                            if (r<=this.probabilities[2])
                            {
                                spawned = true;
                                en = this.grid.GetComponent<GridManager>().mediumEnemy1;
                                break;
                            }
                            r = Random.Range(0,101);
                            if (r<=this.probabilities[1])
                            {
                                spawned = true;
                                en = this.grid.GetComponent<GridManager>().smallEnemy2;
                                break;
                            }
                            r = Random.Range(0,101);
                            if (r<=this.probabilities[0])
                            {
                                spawned = true;
                                en = this.grid.GetComponent<GridManager>().smallEnemy1;
                                break;
                            } 
                    }
                    Vector3 pos = t.GetComponent<Tile>().transform.position;
                    pos.z = -5;
                    GameObject enemy = Instantiate(en, pos, Quaternion.identity);
                    enemy.GetComponent<EnemyMovement>().setResourceManager(resourceManager);
                    this.cooldown = en.GetComponent<EnemyMovement>().cooldown / (1 + (resourceManager.GetComponent<ResourceManagerScript>().getLevel()-1)/10);
                    break;
                
            }


        }
        

    }
    public void setResourceManager(GameObject manager)
    {this.resourceManager = manager;}
    public void setTurret(GameObject t)
    {this.turret = t;}
    public GameObject getTurret() {return this.turret;}
    void setProbabilities()
    {
        int level = this.resourceManager.GetComponent<ResourceManagerScript>().getLevel();
        if (level<=3)
        {
            this.probabilities = new float[] {100f, 0f, 0f, 0f, 0f, 0f};
        }
        if (level>3 && level<=5)
        {
            this.probabilities = new float[] {80f, 20f, 0f, 0f, 0f, 0f};
        }
        if (level>5 && level<=8)
        {
            this.probabilities = new float[] {60f, 30f, 10f, 10f, 0f, 0f};
        }
        if (level>8 && level<=13)
        {
            this.probabilities = new float[] {40f, 30f, 20f, 20f, 1f, 1f};
        }
        if (level>13 && level<=20)
        {
            this.probabilities = new float[] {40f, 30f, 20f, 20f, 5f, 5f};
        }
        if (level>20 && level<=27)
        {
            this.probabilities = new float[] {20f, 20f, 40f, 40f, 15f, 15f};
        }
        if (level>=28)
        {
            this.probabilities = new float[] {5f, 5f, 40f, 40f, 30f, 30f};
        }



    }
}
