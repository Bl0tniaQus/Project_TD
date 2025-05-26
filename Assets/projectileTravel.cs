using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileTravel : MonoBehaviour
{
    public float speed;
    public float ttl;
    Vector3 target;
    float lifetime = 0;
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
        this.transform.position += target * speed;
        if (this.transform.position == target) {Destroy(this.gameObject);}
        lifetime+=Time.deltaTime;
        if (lifetime>=ttl) {Destroy(this.gameObject);}
    }
    public void setTarget(Vector3 t)
    {
        this.target = t;
    }
}
