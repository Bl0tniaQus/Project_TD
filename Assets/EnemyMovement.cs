using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float initial_speed;
    private float max_speed;
    public int damage;
    public int health;
    public float cooldown;
    int max_health;
    GameObject resourceManager;
    float speed_r = 0.0f;
    float speed_l = 0.0f;
    float speed_u = 0.0f;
    float speed_d = 0.0f;
    bool markedForDestruction = false;
    Transform t = null;
    char direction = 'f';
    int step;
    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        step = Random.Range(100,240);
        health = (int)(health * (1 + resourceManager.GetComponent<ResourceManagerScript>().getLevel()/20));
        max_health = health;
        max_speed = initial_speed;
    }

    // Update is called once per frame
    void Update()
    {
        max_speed = initial_speed * (1 + (resourceManager.GetComponent<ResourceManagerScript>().getLevel()/100)*3);
        if ((this.direction != 'r')&&(speed_r > 0))
        {
            speed_r = speed_r - max_speed / step;
            if (speed_r<0) {speed_r = 0.0f;}
        }
        if ((this.direction != 'l')&&(speed_l > 0))
        {
            speed_l = speed_l - max_speed / step;
            if (speed_l<0) {speed_l = 0.0f;}
        }
        if ((this.direction != 'u')&&(speed_u > 0))
        {
            speed_u = speed_u - max_speed / step;
            if (speed_u<0) {speed_u = 0.0f;}
        }
        if ((this.direction != 'd')&&(speed_d > 0))
        {
            speed_d = speed_d - max_speed / step;
            if (speed_d<0) {speed_d = 0.0f;}
        }

        t.position += Vector3.right * speed_r * Time.deltaTime;
        t.position += Vector3.left * speed_l * Time.deltaTime;
        t.position += Vector3.down * speed_d * Time.deltaTime;
        t.position += Vector3.up * speed_u * Time.deltaTime;
        

    }
    void FixedUpdate()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        GameObject obj = collision.gameObject;
        string name = obj.name;
        if (name=="Tile")
        {
            short type = obj.GetComponent<Tile>().getType();
            if (type==3) {
                this.direction = obj.GetComponent<Tile>().getDirection();
                max_speed = initial_speed * (1 + (resourceManager.GetComponent<ResourceManagerScript>().getLevel()/100)*2);
                if (this.direction == 'r')
                {
                    speed_r = max_speed;
                }
                if (this.direction == 'l')
                {
                    speed_l = max_speed;
                }
                if (this.direction == 'u')
                {
                    speed_u = max_speed;
                }
                if (this.direction == 'd')
                {
                    speed_d = max_speed;
                }
                }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        string name = obj.name;
        if (name=="Tile")
        {
            short type = obj.GetComponent<Tile>().getType();
            if (type==1)
            {
                markedForDestruction = true;
                this.resourceManager.GetComponent<ResourceManagerScript>().takeDamage(this.damage);
                Destroy(this.gameObject);
            }
        }
    }
    public bool getMarkedForDestruction()
    {
        return markedForDestruction;
    }
    public void takeDamage(int dmg)
    {
        health-=dmg;
        if (health<=0)
        {
            markedForDestruction = true;
            this.resourceManager.GetComponent<ResourceManagerScript>().increaseScore(this.damage*2);
            int r = Random.Range(1,1000);
            if (r==711)
            {
                this.resourceManager.GetComponent<ResourceManagerScript>().mapExpansion();
            }
        
            Destroy(this.gameObject);
        }
    }
    public void setResourceManager(GameObject manager)
    {this.resourceManager = manager;}
}
