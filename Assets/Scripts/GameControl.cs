using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public Transform[] spawns;
    public GameObject[] enemies;
    public Text timeCounter;
    public Text enemiesCounterText;
    public Text scoreText;
    public Text finishScoreText;
    public int enemiesCounter;
    public int score;
    public float[][] weapons;

    private int[][] wavesTab;
    private int wavesNumber;
    private int actualWave = 0;
    private float startTime;

    void Start()
    {
        startTime = Time.time;

        weapons = new float[3][];

        //damage, attack rate, bullet speed, life length
        weapons[0] = new float[] { 1.0f, 0.2f, 0.0f, 0.1f };    //melee
        weapons[1] = new float[] { 2.0f, 0.5f, 30.0f, 5.0f };  //pistol
        weapons[2] = new float[] { 5.0f, 1.0f, 20.0f, 5.0f };  //shotgun
        //----------------------------------------------

        wavesTab = new int[10][];
        wavesNumber = wavesTab.Length;

        //sec, ilosci przeciwnikow w kolejnych falach
        wavesTab[0] = new int[] { 0, 4, 0, 0 };
        wavesTab[1] = new int[] { 15, 4, 4, 0 };
        wavesTab[2] = new int[] { 40, 8, 4, 2 };
        wavesTab[3] = new int[] { 120, 4, 10, 2 };
        wavesTab[4] = new int[] { 180, 10, 0, 8 };
        wavesTab[5] = new int[] { 300, 6, 10, 10 };
        wavesTab[6] = new int[] { 400, 0, 20, 0 };
        wavesTab[7] = new int[] { 500, 10, 10, 10 };
        wavesTab[8] = new int[] { 600, 15, 5, 20 };
        wavesTab[9] = new int[] { 700, 20, 20, 20 };
        //----------------------------------------
    }

    void Update()
    {
        timeCounter.text = ((int)(startTime + wavesTab[actualWave][0] - Time.time)).ToString();
        enemiesCounterText.text = enemiesCounter.ToString();
        scoreText.text = score.ToString();
        finishScoreText.text = score.ToString();

        if (actualWave < wavesNumber && Time.time > startTime + wavesTab[actualWave][0])
        {
            spawnWave(actualWave);
            actualWave++;
        }
    }

    void spawnWave(int id)
    {
        for (int i = 1; i < wavesTab[i].Length; i++)
        {
            for (int j = 0; j < wavesTab[id][i]; j++)
            {
                GameObject enemy = Instantiate(enemies[i-1], spawns[j % spawns.Length].position, Quaternion.identity);
                enemiesCounter += 1;
            }
        }
    }
}
