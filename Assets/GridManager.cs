using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Tile prefab;
    public GameObject en;
    public GameObject camera;
    public GameObject mapGrid;
    public int maxDim;
    public int baseBranchProbability;
    public float baseBranchDecay;
    public int baseSpawnerSpawnProbability;
    public float baseSpawnerSpawnDecay;
    private List<(int, int)> roadList = new List<(int, int)>();
    private Tile[,] tiles;
    static int dim;
    private int neutralLevel = 0;
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
            tiles[i,j].GetComponent<Tile>().setCoords(i,j);
            tiles[i,j].GetComponent<Tile>().setGrid(mapGrid);
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

        expandField_left();
        expandField_left();
        expandField_left();
        expandField_left();
        expandField_right();
        expandField_right();
        expandField_right();
        expandField_right();
        expandField_down();
        expandField_down();
        expandField_down();
        expandField_down();
        expandField_up();
        expandField_up();
        expandField_up();
        expandField_up();


        setRoad(center-1, center, 'r');
        setRoad(center-2, center, 'r');
        setRoad(center+1, center, 'l');
        setRoad(center+2, center, 'l');
        setRoad(center, center-1, 'u');
        setRoad(center, center-2, 'u');
        setRoad(center, center+1, 'd');
        setRoad(center, center+2, 'd');
        
        setRoad(center-3, center, 'r');
        setRoad(center+3, center, 'l');
        setRoad(center, center-3, 'u');
        setRoad(center, center+3, 'd');


        //setSpawner(center-1, center+3);
        expandRoad();
        expandRoad();
        expandRoad();
        expandRoad();
        expandRoad();
        expandRoad();
        Debug.Log(roadList.Count);

        //tiles[center-2, center+1].GetComponent<Tile>().setType(3); tiles[center-2, center+1].GetComponent<Tile>().setDirection('d');
        //tiles[center-3, center+1].GetComponent<Tile>().setType(3); tiles[center-3, center+1].GetComponent<Tile>().setDirection('r');
        //tiles[center-3, center+2].GetComponent<Tile>().setType(3); tiles[center-3, center+2].GetComponent<Tile>().setDirection('d');


        //Vector3 pos = tiles[center, center+3].GetComponent<Tile>().transform.position;
        //pos.z = -5;
        //GameObject enemy = Instantiate(en, pos, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void expandField_left()
    {
        if (x_left==0) {return;}
        for (int i = y_bot; i<=y_top; i++)
        {
            tiles[x_left-1, i].setType(2);
        }
        x_left--;
    }
    void expandField_right()
    {
        if (x_right==maxDim) {return;}
        for (int i = y_bot; i<=y_top; i++)
        {
            tiles[x_right+1, i].setType(2);
        }
        x_right++;
    }
    void expandField_up()
    {
        if (y_top==maxDim) {return;}
        for (int i = x_left; i<=x_right; i++)
        {
            tiles[i, y_top + 1].setType(2);
        }
        y_top++;
    }
    void expandField_down()
    {
        if (y_bot==0) {return;}
        for (int i = x_left; i<=x_right; i++)
        {
            tiles[i, y_bot - 1].setType(2);
        }
        y_bot--;
    }
    void addRoadToList(int x, int y)
    {
        roadList.Add((x,y));
    }
    void setRoad(int x, int y, char dir)
    {
        tiles[x,y].GetComponent<Tile>().setType(3);
        tiles[x,y].GetComponent<Tile>().setDirection(dir);
        addRoadToList(x,y);
    }
    void setSpawner(int x, int y)
    {
        tiles[x,y].GetComponent<Tile>().setType(4);
    }
    public Tile getTile(int x, int y)
    {
        return tiles[x,y];
    }
    void expandRoad()
    {
        int new_roads = 0;

        for (int i = roadList.Count-1; i>8; i--)
        {
            (int x, int y) = roadList[i];

            char dir = getRandomDirection();
            
            int prob = Random.Range(0,100);
            float branchProb = weightedProbability(baseBranchProbability, baseBranchDecay, new_roads);
            (int x_new, int y_new) = shiftCoords(x,y,dir);
            if (checkFreeField(x_new,y_new) && prob >= branchProb)
            {
                //Debug.Log(dir);
                setRoad(x_new,y_new, getOppositeDirection(dir));
                new_roads++;
            }

        }
        spawnSpawners();
    }
    public void spawnSpawners()
    {
        int new_spawners = 0;

        for (int i = roadList.Count-1; i>8; i--)
        {
            (int x, int y) = roadList[i];
            char dir = getRandomDirection();
            int prob = Random.Range(0,100);
            float spawnProb = weightedProbability(baseSpawnerSpawnProbability, baseSpawnerSpawnDecay, new_spawners);
            (int x_new, int y_new) = shiftCoords(x,y,dir);
            if (checkFreeField(x_new,y_new) && prob >= spawnProb)
            {
                setSpawner(x_new,y_new);
                new_spawners++;
            }

        }
    }
    public char getRandomDirection()
    {
        int dir = Random.Range(1,4);
        if (dir == 1) {return 'r';}
        else if (dir == 2) {return 'l';}
        else if (dir == 3) {return 'd';}
        else if (dir == 4) {return 'u';}
        return 'f';
    }
    public char getOppositeDirection(char dir)
    {
        if (dir == 'r') {return 'l';}
        else if (dir == 'l') {return 'r';}
        else if (dir == 'u') {return 'd';}
        else if (dir == 'd') {return 'u';}
        return 'f';
    }
    public float distFromCenter(int x, int y)
    {
        return ((x-center) * (x-center)) + ((y - center) * (y - center));
    }
    public bool checkFreeField(int x, int y)
    {
         if (x<0 || x>maxDim) {return false;}
         else if (y<0 || y>maxDim) {return false;}
         else if (tiles[x,y].GetComponent<Tile>().getType()!=2) {return false;}
         return true;
    }
    public (int, int) shiftCoords(int x, int y, char dir)
    {
        int x_new = x;
        int y_new = y;
        if (dir=='r') {x_new +=1;}
        if (dir=='l') {x_new -=1;}
        if (dir=='u') {y_new +=1;}
        if (dir=='d') {y_new -= 1;}
        return (x_new, y_new);
    }
    public float weightedProbability(int baseP, float decay, int n)
    {
        return baseP / (1 + Mathf.Pow(decay, n));
    }
}
