using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float health = 100;
    public int weapon = 1;
    public float bulletOffset = 1.0f;


    protected float lastShot;
    protected float currentHealth;
    protected float[][] weapons;
    private Renderer rend;
    private Color newColor;

    protected virtual void Start()
    {
        rend = GetComponent<Renderer>();
        newColor = rend.material.color;
        currentHealth = health;
        lastShot = Time.time - lastShot;

        weapons = new float[3][];

        //damage, attack rate, bullet speed
        weapons[0] = new float[] { 5.0f, 0.1f, 0.0f };    //melee
        weapons[1] = new float[] { 10.0f, 0.5f, 30.0f };  //pistol
        weapons[2] = new float[] { 20.0f, 0.7f, 20.0f };  //shotgun
        //---------------------------------
    }

    protected virtual void Update()
    {

    }

    public void dealDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            kill();
        }
        else
        {
            updateColor(damage);
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
        bulletSc.damage = weapons[weapon][0];

        bullet.transform.position = transform.position + direction * bulletOffset;
        bulletRb.velocity = direction * weapons[weapon][2];
    }
}
