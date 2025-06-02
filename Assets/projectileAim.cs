using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileAim : MonoBehaviour
{
    public float initialCooldown;
    public GameObject projectile;
    float cooldown;
    GameObject closestEnemy = null;
    GameObject resourceManager;
    private Tile tile;
    float x,y;
    float closestDist = -1.0f;
    short level = 1;
    public int damage;
    public float speed;
    public float ttl;
    public int piercing;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = this.transform.position;
        x = pos.x;
        y = pos.y;
        cooldown = initialCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (closestEnemy!=null&&this.name!="EMPTower(Clone)")
        {
            Vector2 direction = closestEnemy.transform.position - this.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (this.cooldown>0f) {this.cooldown-=Time.deltaTime;}
        if (this.cooldown<=0f)
        {
            
            if (closestEnemy!=null)
            {
                Vector3 pos = this.transform.position;
                pos.z = -5;
                GameObject bullet = Instantiate(projectile, pos, Quaternion.identity);
                bullet.GetComponent<projectileTravel>().setResourceManager(resourceManager);
                bullet.GetComponent<projectileTravel>().setDamage(damage);
                bullet.GetComponent<projectileTravel>().setSpeed(speed);
                bullet.GetComponent<projectileTravel>().setTtl(ttl);
                bullet.GetComponent<projectileTravel>().setPiercing(piercing);
                bullet.GetComponent<projectileTravel>().setTarget((closestEnemy.transform.position - this.transform.position).normalized);
                Vector2 direction = closestEnemy.transform.position - bullet.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                closestEnemy=null;
                closestDist=-1.0f;
                this.cooldown = initialCooldown;
            }
            
        }
        
    }
    void FixedUpdate()
    {
        
    }
    private float dist(GameObject enemy)
    {
        Vector3 pos = enemy.transform.position;
        float xe = pos.x;
        float ye = pos.y;

        return ((xe - x) * (xe - x)) + ((ye - y) * (ye - y));

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        GameObject obj = collision.gameObject;
        string tag = obj.tag;
        if (tag=="Enemy")
        {
            if (obj.GetComponent<EnemyMovement>().getMarkedForDestruction()) {return;}
            float distance = dist(obj);
            if ((distance < closestDist) || (closestEnemy == null))
            {
                closestEnemy = obj;
                closestDist = distance;
            }
        }
    }
    public void setResourceManager(GameObject manager) {this.resourceManager = manager;}
    public short getLevel() {return this.level;}
    public void setTile(Tile t) {this.tile = t;}
    public void upgrade()
    {
        
        if (this.level<3)
        {
            string name = this.name;
            this.level++;
            this.tile.GetComponent<Animator>().SetInteger("Type", 50+level);
            if (name=="Energy Blaster(Clone)")
        {
            this.initialCooldown-=0.4f;
            this.damage+=3*this.level;
            this.speed+=0.06f*this.level;
            this.GetComponent<CircleCollider2D>().radius += 1;
        }
        if (name=="Precision Laser(Clone)")
        {
            this.initialCooldown-=0.6f;
            this.damage+=6*this.level;
            this.speed+=0.3f*this.level;
            this.piercing+=1;
            this.GetComponent<CircleCollider2D>().radius += 1*this.level;
        }
        if (name=="EMPTower(Clone)")
        {
            this.damage+=2*this.level;
            this.speed+=0.5f*this.level;
            this.GetComponent<CircleCollider2D>().radius += 0.3f * this.level;
        }
        }
        
        

    }
}
