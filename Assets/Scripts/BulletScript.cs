using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 10;
    public float lifeLength = 10.0f;

    private float createdTime;
    
    void Start() {
        createdTime = Time.time;
    }

    void Update()
    {
        if(Time.time - createdTime > lifeLength)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);

        if(other.gameObject.tag.Equals("Enemy"))
        {
            other.GetComponent<EnemyScript>().dealDamage(damage);
        }
    }
}
