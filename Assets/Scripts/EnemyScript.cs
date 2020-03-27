using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : UnitScript
{
    public int scoreValue = 10;
    public NavMeshAgent agent;
    public float minStoppingDistance = 2.2f;

    private GameObject enemies;
    private GameObject player;
    private RaycastHit hit;
    private LayerMask layerMask;
    private float stoppingDistance;


    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        enemies = GameObject.Find("Enemies");
        transform.parent = enemies.transform;
        stoppingDistance = agent.stoppingDistance;
        layerMask = ~((1 << 8) ^ (1 << 10));
    }
    protected override void Update()
    {
        base.Update();
        agentControl();
    }

    protected override void kill()
    {
        base.kill();
        enemySpawner.enemiesCounter -= 1;
        enemySpawner.score += scoreValue;
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

                    if (Time.time - lastShot > enemySpawner.weapons[weapon][1])
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
