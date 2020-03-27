using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 10;
    public float lifeLength = 3.0f;
    public string shooterTag;

    private float createdTime;

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
        else if(!other.gameObject.tag.Equals("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
