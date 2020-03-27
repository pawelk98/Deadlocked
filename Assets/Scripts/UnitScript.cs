using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float health = 100;
    public int weapon = 1;
    public float bulletOffset = 1.0f;


    protected float lastShot;
    protected float currentHealth;
    protected GameControl enemySpawner;
    private Renderer rend;
    private Color newColor;
    private bool isDead = false;

    protected virtual void Start()
    {
        rend = GetComponent<Renderer>();
        newColor = rend.material.color;
        currentHealth = health;
        lastShot = Time.time - lastShot;
        enemySpawner = GameObject.Find("Enemies").GetComponent<GameControl>();

    }

    protected virtual void Update()
    {

    }

    public void dealDamage(float damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                isDead = true;
                kill();
            }
            else
            {
                updateColor(damage);
            }
        }
    }

    private void updateColor(float damage)
    {
        float colorChange = damage / health;

        newColor.r += colorChange;
        newColor.g += colorChange;
        newColor.b += colorChange;

        if (newColor.r > 1)
            newColor.r = 1;

        if (newColor.g > 1)
            newColor.g = 1;

        if (newColor.b > 1)
            newColor.b = 1;

        rend.material.color = newColor;
    }

    protected virtual void kill()
    {
        Destroy(gameObject);
    }

    protected void shoot(Vector3 direction)
    {
        switch (weapon)
        {
            case 2:
                singleShoot(direction);
                singleShoot(Quaternion.AngleAxis(-15, Vector3.up) * direction);
                singleShoot(Quaternion.AngleAxis(-7.5f, Vector3.up) * direction);
                singleShoot(Quaternion.AngleAxis(7.5f, Vector3.up) * direction);
                singleShoot(Quaternion.AngleAxis(15, Vector3.up) * direction);
                break;

            default:
                singleShoot(direction);
                break;
        }

        lastShot = Time.time;
    }

    private void singleShoot(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        BulletScript bulletSc = bullet.GetComponent<BulletScript>();

        bulletSc.shooterTag = gameObject.tag;
        bulletSc.damage = enemySpawner.weapons[weapon][0];
        bulletSc.lifeLength = enemySpawner.weapons[weapon][3];

        bullet.transform.position = transform.position + direction * bulletOffset;
        bulletRb.velocity = direction * enemySpawner.weapons[weapon][2];
    }
}
