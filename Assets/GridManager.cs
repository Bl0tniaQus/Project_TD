using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Tile prefab;
    public GameObject en;
    public GameObject camera;
    public GameObject mapGrid;
    public GameObject EB_prefab;
    public GameObject PL_prefab;
    public GameObject ET_prefab;
    public GameObject resourceManager;
    public GameObject uiManager;
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
    private GameObject highlightedField = null;
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
            tiles[i,j].GetComponent<Tile>().setResourceManager(resourceManager);
            tiles[i,j].GetComponent<Animator>().SetInteger("Type", 0);
            //tiles[i,j].GetComponent<Tile>().setType(1);
            //tiles[i,j].GetComponent<SpriteRenderer>().material.color = new Color(0, 204, 102);
            //tiles[i,j].GetComponent<SpriteRenderer>().material.color = new Color(0, 204, 102);
        }
        
        }
        tiles[center, center].GetComponent<Tile>().setType(1);
        Debug.Log(center);
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
        
        setRoad(center, center-3, 'u');
        setRoad(center, center+3, 'd');
        setRoad(center+3, center, 'l');
        setTurret(1, center-1,center-1);
        setTurret(2, center+1,center+1);
        setTurret(3, center-1,center+1);
        setTurret(3, center+1,center-1);
        //setSpawner(center-1, center+3);
        expandRoad();
        expandRoad();
        expandRoad();
        expandRoad();
        expandRoad();
        expandRoad();

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
    public void expandField_left()
    {
        if (x_left==0) {return;}
        for (int i = y_bot; i<=y_top; i++)
        {
            tiles[x_left-1, i].setType(2);
            setFloor(x_left-1, i);
        }
        x_left--;
    }
    public void expandField_right()
    {
        if (x_right==maxDim) {return;}
        for (int i = y_bot; i<=y_top; i++)
        {
            setFloor(x_right+1, i);
        }
        x_right++;
    }
    public void expandField_up()
    {
        if (y_top==maxDim) {return;}
        for (int i = x_left; i<=x_right; i++)
        {
            tiles[i, y_top + 1].setType(2);
            setFloor(i, y_top + 1);
        }
        y_top++;
    }
    public void expandField_down()
    {
        if (y_bot==0) {return;}
        for (int i = x_left; i<=x_right; i++)
        {
            setFloor(i, y_bot-1);
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


        int a = 30;
        if (dir=='r') {a = 30;}
        if (dir=='d') {a = 31;}
        if (dir=='l') {a = 32;}
        if (dir =='u') {a = 33;}

        tiles[x,y].GetComponent<Animator>().SetInteger("Type", a);

        addRoadToList(x,y);
    }
    void setSpawner(int x, int y)
    {
        tiles[x,y].GetComponent<Tile>().setType(4);
        tiles[x,y].GetComponent<SpriteRenderer>().color = Color.green;
    }
    public Tile getTile(int x, int y)
    {
        return tiles[x,y];
    }
    public void expandRoad()
    {
        int new_roads = 0;

        for (int i = roadList.Count-1; i>7; i--)
        {
            (int x, int y) = roadList[i];
            int contProb = Random.Range(1,11);
            char dir;
            if (contProb <= 2) {
                dir = getOppositeDirection(getTile(x,y).GetComponent<Tile>().getDirection());
                }
            else {dir = getRandomDirection();}
            
            
            int prob = Random.Range(0,101);
            float branchProb = weightedProbability(baseBranchProbability, baseBranchDecay, new_roads);
            (int x_new, int y_new) = shiftCoords(x,y,dir);
            if (checkFreeField(x_new,y_new) && prob <= branchProb)
            {
                setRoad(x_new,y_new, getOppositeDirection(dir));
                new_roads++;
            }

        }
        spawnSpawners();
    }
    public void spawnSpawners()
    {
        int new_spawners = 0;

        for (int i = roadList.Count-1; i>7; i--)
        {
            (int x, int y) = roadList[i];
            char dir = getRandomDirection();
            int prob = Random.Range(0,100);
            float spawnProb = weightedProbability(baseSpawnerSpawnProbability, baseSpawnerSpawnDecay, new_spawners);
            (int x_new, int y_new) = shiftCoords(x,y,dir);
            if (checkFreeField(x_new,y_new) && prob <= spawnProb)
            {
                setSpawner(x_new,y_new);
                new_spawners++;
            }

        }
    }
    public char getRandomDirection()
    {
        int dir = Random.Range(1,5);
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
    public void setTurret(short type, int x, int y)
    {
         tiles[x,y].GetComponent<Tile>().setType(5);
         Vector3 pos = tiles[x,y].GetComponent<Tile>().transform.position;
         pos.z = -9;
        if (type==1)
        {
            GameObject turret = Instantiate(EB_prefab, pos, Quaternion.identity);
            turret.GetComponent<projectileAim>().setResourceManager(resourceManager);
            tiles[x,y].GetComponent<Tile>().setTurret(turret);
        }
        if (type==2)
        {
            GameObject turret = Instantiate(PL_prefab, pos, Quaternion.identity);
            turret.GetComponent<projectileAim>().setResourceManager(resourceManager);
            tiles[x,y].GetComponent<Tile>().setTurret(turret);
        }
        if (type==3)
        {
            GameObject turret = Instantiate(ET_prefab, pos, Quaternion.identity);
            turret.GetComponent<projectileAim>().setResourceManager(resourceManager);
            tiles[x,y].GetComponent<Tile>().setTurret(turret);
        }
    }
    public void setFloor(int x, int y)
    {
        tiles[x,y].GetComponent<Tile>().setType(2);
        int r = Random.Range(0,5);
        tiles[x,y].GetComponent<Animator>().SetInteger("Type", 20+r);
    }
    public void highlightField(GameObject f)
    {
        if (this.highlightedField!=null) {
            this.highlightedField.GetComponent<SpriteRenderer>().color = Color.white;
            }

        this.highlightedField = f;
        this.highlightedField.GetComponent<SpriteRenderer>().color = Color.yellow;

        short type = f.GetComponent<Tile>().getType();
        if (type==5)
        {
            this.uiManager.GetComponent<UIManager>().ShowUpgradePanel(f);
        }
        else if (type==2)
        {
            this.uiManager.GetComponent<UIManager>().ShowBuildPanel(f);
        }
        else 
        {
            this.uiManager.GetComponent<UIManager>().CloseAllPanels();
        }


    }
}
