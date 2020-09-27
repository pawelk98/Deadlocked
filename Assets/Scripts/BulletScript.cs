using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public ParticleSystem particle;
    public MeshRenderer meshRenderer;
    public Light lights;
    public Rigidbody rb;
    public bool isEmitting = false;

    float damage = 10;
    float lifeLength = 3.0f;
    string shooterTag;
    float createdTime;
    bool hasParticles;

    void Start()
    {
        transform.parent = GameObject.Find("Bullets").transform;
        createdTime = Time.time;
        hasParticles = isEmitting;
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
        if(!hasParticles || isEmitting && meshRenderer.enabled)
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
                if(isEmitting)
                {
                    rb.velocity = Vector3.zero;
                    meshRenderer.enabled = false;
                    lights.enabled = false;
                    particle.Play();
                }
            }
        }
    }

    public void setValues(float damage, float lifeLength, string shooterTag)
    {
        this.damage = damage;
        this.lifeLength = lifeLength;
        this.shooterTag = shooterTag;
    }
}
