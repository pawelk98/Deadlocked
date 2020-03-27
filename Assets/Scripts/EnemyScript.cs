using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : UnitScript
{
    public NavMeshAgent agent;
    public float minStoppingDistance = 2.2f;

    private GameObject player;
    private RaycastHit hit;
    private LayerMask layerMask;
    private float stoppingDistance;


    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        transform.parent = GameObject.Find("Enemies").transform;
        stoppingDistance = agent.stoppingDistance;
        layerMask = ~((1 << 8) ^ (1 << 10));
    }
    protected override void Update()
    {
        base.Update();
        agentControl();
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

                    if (Time.time - lastShot > weapons[weapon][1])
                    {
                        shoot((player.transform.position - transform.position).normalized);
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
