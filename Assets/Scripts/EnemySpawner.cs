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
        
        wavesData = new int[38][];

        wavesData[0] = new int[] { 2, 0, 0, 0 }; 
        wavesData[1] = new int[] { 4, 0, 0, 0 };
        wavesData[2] = new int[] { 2, 1, 0, 0 };
        wavesData[3] = new int[] { 2, 2, 0, 0 };
        wavesData[4] = new int[] { 0, 4, 0, 0 };
        wavesData[5] = new int[] { 2, 4, 0, 0 };
        wavesData[6] = new int[] { 8, 0, 2, 0 };
        wavesData[7] = new int[] { 2, 0, 1, 0 };
        wavesData[8] = new int[] { 5, 5, 0, 0 };
        wavesData[9] = new int[] { 4, 8, 0, 0 };
        wavesData[10] = new int[] { 0, 0, 4, 0 };
        wavesData[11] = new int[] { 10, 0, 2, 0 };
        wavesData[12] = new int[] { 8, 4, 2, 0 };
        wavesData[13] = new int[] { 0, 0, 0, 1 };
        wavesData[14] = new int[] { 15, 0, 0, 0 };
        wavesData[15] = new int[] { 5, 10, 1, 0 };
        wavesData[16] = new int[] { 5, 5, 0, 1 };
        wavesData[17] = new int[] { 8, 8, 4, 0 };
        wavesData[18] = new int[] { 6, 10, 2, 1 };
        wavesData[19] = new int[] { 10, 0, 2, 2 };
        wavesData[20] = new int[] { 0, 0, 10, 0 };
        wavesData[21] = new int[] { 0, 0, 0, 4 };
        wavesData[22] = new int[] { 10, 5, 10, 0 };
        wavesData[23] = new int[] { 20, 0, 0, 1 };
        wavesData[24] = new int[] { 10, 10, 10, 0 };
        wavesData[25] = new int[] { 5, 4, 10, 2 };
        wavesData[26] = new int[] { 10, 8, 0, 5 };
        wavesData[27] = new int[] { 20, 10, 10, 0 };
        wavesData[28] = new int[] { 0, 20, 0, 0 };
        wavesData[29] = new int[] { 0, 0, 0, 10 };
        wavesData[30] = new int[] { 20, 0, 15, 0 };
        wavesData[31] = new int[] { 20, 15, 4, 0 };
        wavesData[32] = new int[] { 15, 10, 10, 1 };
        wavesData[33] = new int[] { 15, 10, 7, 4 };
        wavesData[34] = new int[] { 15, 10, 7, 4 };
        wavesData[35] = new int[] { 15, 15, 15, 4 };
        wavesData[36] = new int[] { 0, 20, 20, 6 };
        wavesData[37] = new int[] { 20, 20, 20, 10 };
    }

    void Update()
    {
        if(uIController.EnemiesNumber == 0)
        {
            if(nextWave < wavesData.Length)
            {
                spawnWave(nextWave++);
            }
            else
            {
                spawnWave(wavesData.Length - 1);
            }
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
