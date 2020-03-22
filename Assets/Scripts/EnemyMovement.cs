using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;


    void Start() {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();
    }
    void Update()
    {
        agent.SetDestination(player.position);
    }
}
