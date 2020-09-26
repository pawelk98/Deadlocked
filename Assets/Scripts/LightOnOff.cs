using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour
{
    public Light lights;
    public int blinkChance = 50;
    public float blinkInterval = 0.5f;
    public float blinkLength = 0.5f;

    float lastCheck;
    float lastBlink;

    void Start()
    {
        lastCheck = Time.time;
    }

    void Update()
    {
        if (Time.time - lastCheck >= blinkInterval)
        {
            lastCheck = Time.time;

            if (lights.enabled)
            {
                if (Random.Range(0, 100) < blinkChance)
                {
                    lights.enabled = false;
                    lastBlink = lastCheck;
                }
            }
        }
        if (Time.time - lastBlink >= blinkLength)
        {
            lights.enabled = true;
        }
    }
}
