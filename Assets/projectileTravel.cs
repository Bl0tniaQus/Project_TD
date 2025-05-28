using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileTravel : MonoBehaviour
{
    public float speed;
    public float ttl;
    public int damage;
    public int piercing;
    Vector3 target;
    float lifetime = 0;
    GameObject resourceManager;
    // Start is called before the first frame update
    void Awake()
    {
        target = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (this.name=="EMP(Clone)")
        {
            this.transform.localScale += new Vector3(speed,speed,0) * Time.deltaTime;
        }
        else
        {
            this.transform.position += target * speed;
            //if (this.transform.position == target) {Destroy(this.gameObject);}      
        }
        lifetime+=Time.deltaTime;
        if (lifetime>=ttl) {Destroy(this.gameObject);}

    }
    public void setTarget(Vector3 t)
    {
        this.target = t;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        string tag = obj.tag;
        if (tag=="Enemy")
        {
            obj.GetComponent<EnemyMovement>().takeDamage(damage);
            piercing--;
            if (piercing<0 && piercing+1>-100) {Destroy(this.gameObject);}
        }
    }
    public void setResourceManager(GameObject manager)
    {this.resourceManager = manager;}
}
