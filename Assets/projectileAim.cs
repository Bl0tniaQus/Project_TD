using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileAim : MonoBehaviour
{
    public float initialCooldown;
    public GameObject projectile;
    float cooldown;
    GameObject closestEnemy = null;
    float x,y;
    float closestDist = -1.0f;
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
        //this.transform.LookAt(closestEnemy.transform.position);
        
    }
    void FixedUpdate()
    {
        Debug.Log(closestDist);
        if (this.cooldown>=0f) {this.cooldown-=Time.deltaTime;}
        else 
        {
            
            if (closestEnemy!=null)
            {
                Vector3 pos = this.transform.position;
                pos.z = -5;
                GameObject bullet = Instantiate(projectile, pos, Quaternion.identity);
                bullet.GetComponent<projectileTravel>().setTarget((closestEnemy.transform.position - this.transform.position).normalized);
                
            }
            this.cooldown = initialCooldown;
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
            float distance = dist(obj);
            if ((distance < closestDist) || (closestDist==-1.0f))
            {
                closestEnemy = obj;
                closestDist = distance;
            }
        }
    }
}
