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
    float x,y;
    float closestDist = -1.0f;
    short level;
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
        
        
        
    }
    void FixedUpdate()
    {
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
                closestEnemy=null;
                closestDist=-1.0f;
                this.cooldown = initialCooldown;
            }
            
        }
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
    public void upgrade()
    {
        string name = this.name;

        if (name=="Energy Blaster(Clone)")
        {
            this.initialCooldown-=0.2f;
            this.damage+=3;
            this.speed+=0.05f;
            this.ttl+=2.5f;
            this.GetComponent<CircleCollider2D>().radius += 2;
        }
        if (name=="Precision Laser(Clone)")
        {
            this.initialCooldown-=0.2f;
            this.damage+=5;
            this.speed+=0.2f;
            this.ttl+=1;
            this.piercing+=1;
            this.GetComponent<CircleCollider2D>().radius += 3;
        }
        if (name=="EMPTower(Clone)")
        {
            this.damage+=1;
            this.speed+=2.5f;
            this.GetComponent<CircleCollider2D>().radius += 1.3f;
        }
        

    }
}
