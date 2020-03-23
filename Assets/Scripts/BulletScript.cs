using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage = 10;
    public float lifeLength = 3.0f;
    public string shooterTag;

    private float createdTime;

    void Start()
    {
        createdTime = Time.time;
    }

    void Update()
    {
        if (Time.time - createdTime > lifeLength)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Player"))
        {
            if (!other.gameObject.tag.Equals(shooterTag))
            {
                Destroy(gameObject);
                other.GetComponent<UnitScript>().dealDamage(damage);
            }
        }
        else if (!other.gameObject.tag.Equals("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
