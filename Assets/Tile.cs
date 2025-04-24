using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private short type; //0 - empty, 1 - core, 2 - grass, 3 - road, 4 - spawner, 10+ - turrets
    private char direction;
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
    public void setType(short type)
    {
        this.type = type;
        if (type==0) {this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);}
        else if (type==1) {this.GetComponent<SpriteRenderer>().color = new Color(255,0,0);}
        else if (type==2) {this.GetComponent<SpriteRenderer>().color = new Color(0,255,0);}
        


    }
    public void setDirection(char direction)
    {
        this.direction = direction;
    }
}
