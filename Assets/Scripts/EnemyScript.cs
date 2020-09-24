using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : UnitScript
{
    public Animator animator;
    public NavMeshAgent agent;
    public GameObject ammo;
    public int scoreValue = 10;
    public float minStoppingDistance = 2.2f;
    public int dropChance = 50;
    public int dropAmount = 10;
    public int dropWeapon = 2;

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
        if(weapon > 0) {
            if(Random.Range(0,100) < dropChance) {
                GameObject spawnedAmmo = Instantiate(ammo, transform.position, Quaternion.AngleAxis(90, Vector3.right));
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
                    animator.SetBool("isWalking", false);  
                    Vector3 rayDirection = player.transform.position - transform.position;
                    rayDirection.y = 0;

                    if (Physics.Raycast(transform.position, rayDirection, out hit, Mathf.Infinity, layerMask)
                        && hit.collider.tag.Equals("Player"))
                    {
                        agent.stoppingDistance = stoppingDistance;
                        agent.velocity = Vector3.zero;

                        if (Time.time - lastShot > weaponsScript.getAttackRate(weapon))
                        {
                            animator.SetBool("isAttacking", true);
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
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                }
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
