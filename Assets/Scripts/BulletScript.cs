using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float damage = 10;
    float lifeLength = 3.0f;
    string shooterTag;
    float createdTime;

    void Start()
    {
        transform.parent = GameObject.Find("Bullets").transform;
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
        else if(!other.gameObject.tag.Equals("Bullet") && !other.gameObject.tag.Equals("Ammo"))
        {
            Destroy(gameObject);
        }
    }

    public void setValues(float damage, float lifeLength, string shooterTag)
    {
        this.damage = damage;
        this.lifeLength = lifeLength;
        this.shooterTag = shooterTag;
    }
}
