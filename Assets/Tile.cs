using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject grid;
    private short type; //0 - empty, 1 - core, 2 - grass, 3 - road, 4 - spawner, 10+ - turrets
    private char direction;
    private int x, y;
    private float cooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        setType(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        
        if (this.cooldown>0f) {this.cooldown-=Time.deltaTime;}
        else 
        {
            Debug.Log("xd");
            if (type==4) {spawn();}
        }

    }
    public void setType(short type)
    {
        this.type = type;
        if (type==0) {this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);}
        else if (type==1) {this.GetComponent<SpriteRenderer>().color = new Color(255,0,0);}
        else if (type==2) {this.GetComponent<SpriteRenderer>().color = new Color(0,255,0);}
        else if (type==3) {this.GetComponent<SpriteRenderer>().color = new Color(128,128,0);}
        else if (type==4) {this.GetComponent<SpriteRenderer>().color = new Color(180,140,128);}


        


    }
    public void setDirection(char direction)
    {
        this.direction = direction;
    }
    public void setCoords(int x,int y) {this.x = x; this.y = y;}
    public char getDirection() {return this.direction;}
    public short getType() {return this.type;}
    public void setCooldown(float cd) {this.cooldown = cd;}
    public void setGrid(GameObject g) {this.grid = g;}
    private void spawn()
    {
        int maxDim = grid.GetComponent<GridManager>().maxDim;
        int x_spawn=0, y_spawn=0;
        while(true)
        {
            
            int dir = Random.Range(1,4);
            if (dir==1) {x_spawn = this.x+1; y_spawn = this.y;}
            else if (dir==2) {x_spawn = this.x-1; y_spawn = this.y;}
            else if (dir==3) {x_spawn = this.x; y_spawn = this.y+1;}
            else if (dir==4) {x_spawn = this.x; y_spawn = this.y-1;}

            if (x_spawn < 0 || x_spawn > maxDim || y_spawn < 0 || y_spawn > maxDim)
            {
                continue;
            }
            else
            {
                Tile t = this.grid.GetComponent<GridManager>().getTile(x_spawn, y_spawn);
                int type = t.GetComponent<Tile>().getType();
                if (type!=3) {continue;}
                else
                {
                    Vector3 pos = t.GetComponent<Tile>().transform.position;
                    pos.z = -5;
                    GameObject enemy = Instantiate(this.grid.GetComponent<GridManager>().en, pos, Quaternion.identity);
                    this.cooldown = 3f;
                    break;
                }
            }


        }
        

    }
}
