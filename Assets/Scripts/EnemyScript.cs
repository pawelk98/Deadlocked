using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : UnitScript
{
    public Animator animator;
    public NavMeshAgent agent;
    public GameObject ammo;
    public int scoreValue = 10;
    public float minStoppingDistance = 2.2f;
    public float rotationSpeed = 10;
    public int dropChance = 50;
    public int dropAmount = 10;
    public int dropWeapon = 2;
    public float dropOffsetY;

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
        layerMask = ~((1 << 8) ^ (1 << 10) ^ (1 << 11));
    }
    protected override void Update()
    {
        base.Update();
        agentControl();
    }

    protected override void kill()
    {
        if(dropWeapon != -1) {
            if(Random.Range(0,100) < dropChance) {
                Vector3 dropPos = new Vector3(transform.position.x, transform.position.y - dropOffsetY + 3, transform.position.z);
                GameObject spawnedAmmo = Instantiate(ammo, dropPos, Quaternion.identity);
                spawnedAmmo.transform.parent = GameObject.Find("Drop").transform;
                spawnedAmmo.GetComponent<AmmoScript>().weapon = dropWeapon;
                spawnedAmmo.GetComponent<AmmoScript>().amount = dropAmount;
            }
        }

        base.kill();
        uIController.EnemiesNumber -= 1;
        uIController.Score += scoreValue;
    }

    void agentControl()
    {
        if (player != null)
        {
            agent.SetDestination(player.transform.position);

            if(agent.hasPath)
            {
                if (agent.remainingDistance <= stoppingDistance)
                {   
                    if(animator)
                    {
                        animator.SetBool("isWalking", false);  
                    }           
                    Vector3 rayDirection = player.transform.position - transform.position;
                    rayDirection.y = 0;

                    if (Physics.Raycast(transform.position, rayDirection, out hit, Mathf.Infinity, layerMask)
                        && hit.collider.tag.Equals("Player"))
                    {
                        agent.stoppingDistance = stoppingDistance;
                        agent.velocity = Vector3.zero;

                        Vector3 direction = (player.transform.position - transform.position).normalized;
                        Quaternion lookRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                        if (Time.time - lastShot > weaponsScript.getAttackRate(weapon))
                        {
                            if(animator)
                            {
                                animator.SetBool("isAttacking", true);
                            }
                            shoot((player.transform.position - transform.position).normalized);
                        }
                    }
                    else
                    {
                        agent.stoppingDistance = minStoppingDistance;
                    }
                }
                else
                {
                    if(animator)
                    {
                        animator.SetBool("isWalking", true);
                        animator.SetBool("isAttacking", false);
                    }
                }
            }
            else
            {
                if(animator)
                {
                    animator.SetBool("isWalking", false);
                }
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
