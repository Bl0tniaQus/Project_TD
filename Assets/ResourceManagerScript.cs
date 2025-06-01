using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManagerScript : MonoBehaviour
{

    private long score = 0;
    private long money = 10000;
    private int hp = 100;
    public double scoreFactor;
    private long scoreGoal = 10;
    private double scoreGoalBase = 10;
    private int level = 0;
    private int coreState = 0;
    public GameObject mapGrid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (score>=scoreGoal) 
        {
                scoreGoalBase = scoreGoalBase * scoreFactor;
                scoreGoal = scoreGoal + (long)(scoreGoalBase);
                level++;
                this.mapGrid.GetComponent<GridManager>().expandRoad();
                int r = Random.Range(1,5);
                if (level>0 && level%2==0) {
                    if (r==1) {this.mapGrid.GetComponent<GridManager>().expandField_down();}
                    if (r==2) {this.mapGrid.GetComponent<GridManager>().expandField_up();}
                    if (r==3) {this.mapGrid.GetComponent<GridManager>().expandField_left();}
                    if (r==4) {this.mapGrid.GetComponent<GridManager>().expandField_right();}
                }
        }
        if (hp<=70&&hp>=31&&coreState==0)
        {
                this.coreState = 1;
                int center = mapGrid.GetComponent<GridManager>().getCenter();
                Tile tile = mapGrid.GetComponent<GridManager>().getTile(center, center);
                tile.GetComponent<Animator>().SetInteger("Type", 11);
        }
        if (hp<=30&&hp>=10&&coreState==1)
        {
                this.coreState = 2;
                int center = mapGrid.GetComponent<GridManager>().getCenter();
                Tile tile = mapGrid.GetComponent<GridManager>().getTile(center, center);
                tile.GetComponent<Animator>().SetInteger("Type", 12);
        }
        if (hp<10&&coreState==2)
        {
                this.coreState = 3;
                int center = mapGrid.GetComponent<GridManager>().getCenter();
                Tile tile = mapGrid.GetComponent<GridManager>().getTile(center, center);
                tile.GetComponent<Animator>().SetInteger("Type", 13);
        }
    }
    public long getScore() {return this.score;}
    public long getMoney() {return this.money;}
    public int getHP() {return this.hp;}
    public void takeDamage(int damage) {
        this.hp-=damage; 
        if (this.hp <= 0)
         {
                this.hp=0;
                mapGrid.GetComponent<GameOverManager>().TriggerGameOver(this.score);
         }
        }
    public void increaseScore(int s) {  this.money+=s; this.score+=s;}
    public int getLevel() {return this.level;}
    public void spendMoney(int m) {this.money -= m;}
}
