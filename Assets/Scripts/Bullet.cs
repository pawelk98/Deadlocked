using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeLength = 10.0f;
    private float created;
    void Start() {
        created = Time.time;
    }
    void Update()
    {
        if(Time.time - created > lifeLength)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
}
