using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float initial_speed;
    public int damage;
    public int health;
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
        step = Random.Range(60,180);
    }

    // Update is called once per frame
    void Update()
    {
 
        

    }
    void FixedUpdate()
    {
        if ((this.direction != 'r')&&(speed_r > 0))
        {
            speed_r = speed_r - initial_speed / step;
            if (speed_r<0) {speed_r = 0.0f;}
        }
        if ((this.direction != 'l')&&(speed_l > 0))
        {
            speed_l = speed_l - initial_speed / step;
            if (speed_l<0) {speed_l = 0.0f;}
        }
        if ((this.direction != 'u')&&(speed_u > 0))
        {
            speed_u = speed_u - initial_speed / step;
            if (speed_u<0) {speed_u = 0.0f;}
        }
        if ((this.direction != 'd')&&(speed_d > 0))
        {
            speed_d = speed_d - initial_speed / step;
            if (speed_d<0) {speed_d = 0.0f;}
        }

        t.position += Vector3.right * speed_r;
        t.position += Vector3.left * speed_l;
        t.position += Vector3.down * speed_d;
        t.position += Vector3.up * speed_u;
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
                if (this.direction == 'r')
                {
                    speed_r = initial_speed;
                }
                if (this.direction == 'l')
                {
                    speed_l = initial_speed;
                }
                if (this.direction == 'u')
                {
                    speed_u = initial_speed;
                }
                if (this.direction == 'd')
                {
                    speed_d = initial_speed;
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
                //TODO deal damage
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
            Destroy(this.gameObject);
        }
    }
}
