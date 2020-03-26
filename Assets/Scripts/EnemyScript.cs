using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : UnitScript
{
    public int type = 1;
    public NavMeshAgent agent;
    public float minStoppingDistance = 2.2f;

    private GameObject player;
    private RaycastHit hit;
    private LayerMask layerMask;
    private float stoppingDistance;


    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        transform.parent = GameObject.Find("Enemies").transform;
        stoppingDistance = agent.stoppingDistance;
        layerMask = ~((1 << 8) ^ (1 << 10));
    }
    public override void Update()
    {
        base.Update();
        agentControl();
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

        lastShot = Time.time;
    }

    void agentControl()
    {
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
                        switch (type)
                        {
                            case 0:
                                player.GetComponent<UnitScript>().dealDamage(base.damage);
                                lastShot = Time.time;
                                break;

                            case 1:
                                shoot();
                                break;

                            case 2:
                                shoot();
                                break;

                            default:
                                player.GetComponent<UnitScript>().dealDamage(base.damage);
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
}
