﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : UnitScript
{
    public int level = 1;
    public int damage = 10;
    public float attackRate = 1.0f;
    public GameObject bulletPrefab;
    public float bulletOffset = 1.0f;
    public float bulletSpeed = 10.0f;
    public NavMeshAgent agent;
    public float minStoppingDistance = 2.2f;

    private GameObject player;
    private RaycastHit hit;
    private LayerMask layerMask;
    private float lastShot;
    private float stoppingDistance;


    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        transform.parent = GameObject.Find("Enemies").transform;
        stoppingDistance = agent.stoppingDistance;
        layerMask = 1 << 8;
        int layerMask2 = 1 << 10;
        layerMask = ~(layerMask ^ layerMask2);
        lastShot = Time.time - lastShot;
    }
    public override void Update()
    {
        base.Update();

        if (player != null)
        {
            agent.SetDestination(player.transform.position);

            if (agent.remainingDistance <= stoppingDistance)
            {
                if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, Mathf.Infinity, layerMask)
                    && hit.collider.tag.Equals("Player"))
                {
                    agent.stoppingDistance = stoppingDistance;
                    agent.velocity = Vector3.zero;

                    if (Time.time - lastShot > attackRate)
                    {
                        lastShot = Time.time;
                        switch (level)
                        {
                            case 1:
                                player.GetComponent<UnitScript>().dealDamage(damage);
                                break;

                            case 2:
                                shoot();
                                break;

                            case 3:
                                shoot();
                                break;

                            default:
                                player.GetComponent<UnitScript>().dealDamage(damage);
                                break;
                        }
                    }
                }
                else
                {
                    agent.stoppingDistance = minStoppingDistance;
                }
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        bullet.GetComponent<BulletScript>().shooterTag = gameObject.tag;
        bullet.GetComponent<BulletScript>().damage = damage;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        bullet.transform.position = transform.position + direction * bulletOffset;
        bulletRb.velocity = direction * bulletSpeed;
    }
}
