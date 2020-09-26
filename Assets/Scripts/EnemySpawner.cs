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
        
        wavesData = new int[14][];

        wavesData[0] = new int[] { 4, 0, 0, 0 };
        wavesData[1] = new int[] { 2, 2, 0, 0 };
        wavesData[2] = new int[] { 5, 2, 0, 0 };
        wavesData[3] = new int[] { 5, 5, 0, 0 };
        wavesData[4] = new int[] { 10, 5, 0, 0 };
        wavesData[5] = new int[] { 15, 7, 0, 0 };
        wavesData[6] = new int[] { 0, 2, 2, 0 };
        wavesData[7] = new int[] { 5, 3, 5, 0 };
        wavesData[8] = new int[] { 0, 15, 5, 0 };
        wavesData[9] = new int[] { 0, 0, 15, 0 };
        wavesData[10] = new int[] { 0, 0, 0, 1 };
        wavesData[11] = new int[] { 30, 0, 0, 0 };
        wavesData[12] = new int[] { 10, 0, 0, 2 };
        wavesData[13] = new int[] { 5, 5, 5, 1 };


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
