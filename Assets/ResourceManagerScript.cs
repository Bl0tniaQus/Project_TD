using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManagerScript : MonoBehaviour
{

    private long score = 0;
    private long money = 0;
    private int hp = 100;
    public GameObject mapGrid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.score);
    }
    public long getScore() {return this.score;}
    public long getMoney() {return this.money;}
    public int getHP() {return this.hp;}
    public void takeDamage(int damage) {this.hp-=damage; if (this.hp<0) {this.hp=0;}}
    public void increaseScore(int s) {  this.money+=s; this.score+=s;}
}
