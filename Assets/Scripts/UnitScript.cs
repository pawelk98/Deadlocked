using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public float health = 100;
    private float currentHealth;
    private Renderer rend;
    private Color newColor;

    void Start() {
        rend = GetComponent<Renderer>();
        newColor = rend.material.color;
        currentHealth = health;
    }
    public void dealDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        updateColor(damage);
    }

    private void updateColor(float damage) 
    {
        float colorChange = damage/health;

        newColor.r += colorChange;
        newColor.g += colorChange;
        newColor.b += colorChange;

        if(newColor.r > 1)
            newColor.r = 1;
        
        if(newColor.g > 1)
            newColor.g = 1;

        if(newColor.b > 1)
            newColor.b = 1;

        rend.material.color = newColor;
    }
}
