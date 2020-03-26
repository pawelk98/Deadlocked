using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int health = 100;
    public int damage = 10;
    public float attackRate = 1.0f;
    public float bulletOffset = 1.0f;
    public float bulletSpeed = 10.0f;

    [HideInInspector]
    public float lastShot;
    [HideInInspector]
    public int currentHealth;


    private Renderer rend;
    private Color newColor;

    public virtual void Start()
    {
        rend = GetComponent<Renderer>();
        newColor = rend.material.color;
        currentHealth = health;
        lastShot = Time.time - lastShot;
    }

    public virtual void Update()
    {

    }
    public void dealDamage(int damage)
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

    private void updateColor(int damage)
    {
        float colorChange = damage / (float)health;

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

    public virtual void kill()
    {
        Destroy(gameObject);
    }
}
