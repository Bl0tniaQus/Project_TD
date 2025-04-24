using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Tile prefab;
    public int maxDim;
    private Tile[,] tiles;
    static int dim;
    private int grassLevel = 0;
    private int center;
    private int x_left,x_right,y_bot,y_top;
    // Start is called before the first frame update
    void Start()
    {
        center = maxDim/2;
        x_left = center;
        x_right = center;
        y_bot = center;
        y_top = center;
        dim = maxDim;
        tiles = new Tile[dim, dim];
        for (int i = 0; i<maxDim; i++)
        {
            for (int j = 0; j<maxDim; j++)
        {
            tiles[i,j] = Instantiate(prefab, new Vector3(i*2,j*2,j), Quaternion.identity);
            //tiles[i,j].GetComponent<Tile>().setType(1);
            //tiles[i,j].GetComponent<SpriteRenderer>().material.color = new Color(0, 204, 102);
            //tiles[i,j].GetComponent<SpriteRenderer>().material.color = new Color(0, 204, 102);
        }
        
        }
        tiles[center, center].GetComponent<Tile>().setType(1);
        expandGrass_left();
        expandGrass_left();
        expandGrass_right();
        expandGrass_down();
        expandGrass_up();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void expandGrass_left()
    {
        if (x_left==0) {return;}
        for (int i = y_bot; i<=y_top; i++)
        {
            tiles[x_left-1, i].setType(2);
        }
        x_left--;
    }
    void expandGrass_right()
    {
        if (x_right==maxDim) {return;}
        for (int i = y_bot; i<=y_top; i++)
        {
            tiles[x_right+1, i].setType(2);
        }
        x_right++;
    }
    void expandGrass_down()
    {
        if (y_bot==maxDim) {return;}
        for (int i = x_left; i<=x_right; i++)
        {
            tiles[i, y_bot + 1].setType(2);
        }
        y_bot++;
    }
    void expandGrass_up()
    {
        if (y_top==0) {return;}
        for (int i = x_left; i<=x_right; i++)
        {
            tiles[i, y_top - 1].setType(2);
        }
        y_top--;
    }
}
