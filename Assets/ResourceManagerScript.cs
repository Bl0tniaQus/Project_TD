using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManagerScript : MonoBehaviour
{

    private long score = 0;
    private long money = 50;
    private int hp = 100;
    public double scoreFactor;
    private long scoreGoal = 10;
    private double scoreGoalBase = 10;
    private int level = 0;
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
                    int r = Random.Range(1,5);
                    if (r==1) {this.mapGrid.GetComponent<GridManager>().expandField_down();}
                    if (r==2) {this.mapGrid.GetComponent<GridManager>().expandField_up();}
                    if (r==3) {this.mapGrid.GetComponent<GridManager>().expandField_left();}
                    if (r==4) {this.mapGrid.GetComponent<GridManager>().expandField_right();}
                
                
                
                this.mapGrid.GetComponent<GridManager>().expandRoad();
                
                
                
        }
    }
    public long getScore() {return this.score;}
    public long getMoney() {return this.money;}
    public int getHP() {return this.hp;}
    public void takeDamage(int damage) {this.hp-=damage; if (this.hp<0) {this.hp=0;}}
    public void increaseScore(int s) {  this.money+=s; this.score+=s;}
    public int getLevel() {return this.level;}
    public void spendMoney(int m) {this.money -= m;}
}
