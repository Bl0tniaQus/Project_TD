using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Tile prefab;
    public GameObject en;
    public GameObject camera;
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
            tiles[i,j] = Instantiate(prefab, new Vector3(i*2,j*2,0), Quaternion.identity);
            tiles[i,j].name = "Tile";
            //tiles[i,j].GetComponent<Tile>().setType(1);
            //tiles[i,j].GetComponent<SpriteRenderer>().material.color = new Color(0, 204, 102);
            //tiles[i,j].GetComponent<SpriteRenderer>().material.color = new Color(0, 204, 102);
        }
        
        }
        tiles[center, center].GetComponent<Tile>().setType(1);

        Vector3 cam_pos = camera.transform.position;
        Vector3 middle_pos = tiles[center, center].GetComponent<Tile>().transform.position;
        cam_pos.x = middle_pos.x;
        cam_pos.y = middle_pos.y;
        camera.transform.position = cam_pos;

        expandGrass_left();
        expandGrass_left();
        expandGrass_left();
        expandGrass_right();
        expandGrass_right();
        expandGrass_down();
        expandGrass_down();
        expandGrass_up();
        expandGrass_up();
        expandGrass_left();
        expandGrass_up();

        tiles[center-1, center].GetComponent<Tile>().setType(3); tiles[center-1, center].GetComponent<Tile>().setDirection('r');
        tiles[center-2, center].GetComponent<Tile>().setType(3); tiles[center-2, center].GetComponent<Tile>().setDirection('r');

        tiles[center+1, center].GetComponent<Tile>().setType(3); tiles[center+1, center].GetComponent<Tile>().setDirection('l');
        tiles[center+2, center].GetComponent<Tile>().setType(3); tiles[center+2, center].GetComponent<Tile>().setDirection('l');

        tiles[center, center-1].GetComponent<Tile>().setType(3); tiles[center, center-1].GetComponent<Tile>().setDirection('u');
        tiles[center, center-2].GetComponent<Tile>().setType(3); tiles[center, center-2].GetComponent<Tile>().setDirection('u');


        tiles[center, center+1].GetComponent<Tile>().setType(3); tiles[center, center+1].GetComponent<Tile>().setDirection('d');
        tiles[center, center+2].GetComponent<Tile>().setType(3); tiles[center, center+2].GetComponent<Tile>().setDirection('d');

        tiles[center-2, center+1].GetComponent<Tile>().setType(3); tiles[center-2, center+1].GetComponent<Tile>().setDirection('d');
        tiles[center-3, center+1].GetComponent<Tile>().setType(3); tiles[center-3, center+1].GetComponent<Tile>().setDirection('r');
        tiles[center-3, center+2].GetComponent<Tile>().setType(3); tiles[center-3, center+2].GetComponent<Tile>().setDirection('d');


        Vector3 pos = tiles[center-3, center+2].GetComponent<Tile>().transform.position;
        pos.z = -5;
        GameObject enemy = Instantiate(en, pos, Quaternion.identity);

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
    void expandGrass_up()
    {
        if (y_top==maxDim) {return;}
        for (int i = x_left; i<=x_right; i++)
        {
            tiles[i, y_top + 1].setType(2);
        }
        y_top++;
    }
    void expandGrass_down()
    {
        if (y_bot==0) {return;}
        for (int i = x_left; i<=x_right; i++)
        {
            tiles[i, y_bot - 1].setType(2);
        }
        y_bot--;
    }
}
