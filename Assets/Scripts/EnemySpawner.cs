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
        startTime = Time.time;

        //czas spawnu, ilosci przeciwnikow
        wavesData = new int[10][];
        wavesData[0] = new int[] { 0, 4, 0, 0 };
        wavesData[1] = new int[] { 15, 4, 4, 0 };
        wavesData[2] = new int[] { 40, 8, 4, 2 };
        wavesData[3] = new int[] { 120, 4, 10, 2 };
        wavesData[4] = new int[] { 180, 10, 0, 8 };
        wavesData[5] = new int[] { 300, 6, 10, 10 };
        wavesData[6] = new int[] { 400, 0, 20, 0 };
        wavesData[7] = new int[] { 500, 10, 10, 10 };
        wavesData[8] = new int[] { 600, 15, 5, 20 };
        wavesData[9] = new int[] { 700, 20, 20, 20 };
        //----------------------------------------
    }

    void Update()
    {
        if (nextWave < wavesData.Length && uIController.Timer <= 0)
        {
            spawnWave(nextWave++);
            uIController.Timer = wavesData[nextWave][0];
        }
    }

    void spawnWave(int id)
    {
        for (int i = 1; i < wavesData[i].Length; i++)
        {
            for (int j = 0; j < wavesData[id][i]; j++)
            {
                GameObject enemy = Instantiate(enemies[i - 1], enemySpawns[j % enemySpawns.Length].position, Quaternion.identity);
                uIController.EnemiesNumber += 1;
            }
        }
    }
}
