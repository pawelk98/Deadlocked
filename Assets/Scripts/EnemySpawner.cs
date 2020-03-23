using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawns;
    public GameObject[] enemies;
    public int wavesNumber = 10;
    public float wavesRate = 10;

    private int[][] wavesTab;
    private float lastWave;
    private int wavesCounter = 0;

    void Start()
    {
        lastWave = Time.time - wavesRate;

        wavesTab = new int[wavesNumber][];

        //ilosci przeciwnikow w kolejnych falach
        wavesTab[0] = new int[] { 4, 0, 0 };
        wavesTab[1] = new int[] { 8, 0, 0 };
        wavesTab[2] = new int[] { 8, 4, 0 };
        wavesTab[3] = new int[] { 4, 10, 0 };
        wavesTab[4] = new int[] { 4, 6, 4 };
        wavesTab[5] = new int[] { 6, 10, 4 };
        wavesTab[6] = new int[] { 4, 0, 10 };
        wavesTab[7] = new int[] { 10, 10, 5 };
        wavesTab[8] = new int[] { 5, 8, 15 };
        wavesTab[9] = new int[] { 10, 15, 10 };
        //--------------------------------------
    }

    void Update()
    {
        if (Time.time - lastWave > wavesRate && wavesCounter <= wavesNumber)
        {
            spawnWave(wavesCounter);

            lastWave = Time.time;
            wavesCounter++;
        }
    }

    void spawnWave(int id)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            for (int j = 0; j < wavesTab[id][i]; j++)
            {
                GameObject enemy = Instantiate(enemies[i], spawns[j % spawns.Length].position, Quaternion.identity);
            }
        }
    }
}
