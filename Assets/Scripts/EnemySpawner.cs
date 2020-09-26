using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public UIController uIController;
    public Transform[] enemySpawns;
    public GameObject[] enemies;

    int[][] wavesData;
    int nextWave;
    float startTime;


    void Start()
    {
        nextWave = 0;
        startTime = Time.time;
        
        wavesData = new int[2][];

        wavesData[0] = new int[] { 1, 5, 0 };
        wavesData[1] = new int[] { 10, 0, 0 };

    }

    void Update()
    {
        if(uIController.EnemiesNumber == 0 && nextWave < wavesData.Length)
        {
            spawnWave(nextWave++);
        }
    }

    void spawnWave(int id)
    {
        int spawnedId = 0;
        for(int i = 0; i < wavesData[id].Length; i++)
        {
            for(int j = 0; j < wavesData[id][i]; j++)
            {
                GameObject enemy = Instantiate(enemies[i], enemySpawns[spawnedId++ % enemySpawns.Length].position, Quaternion.identity);
                uIController.EnemiesNumber += 1;
            }
        }
    }
}
