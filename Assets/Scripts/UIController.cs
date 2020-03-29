using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text timerText;
    public Text enemiesNumberText;
    public Text scoreText;
    public Text finishScoreText;
    public Text healthText;

    public int EnemiesNumber
    {
        get{ return enemiesNumber; }
        set{
            enemiesNumber = value; 
            enemiesNumberText.text = value.ToString(); }
    }

    public float Timer{
        get { return endTime - Time.time; }
        set { endTime = Time.time + value; }
    }

    public int Score{
        get { return score; }
        set {
            score = value;
            scoreText.text = value.ToString();
            finishScoreText.text = value.ToString();
        }
    }

    public float Health{
        set {
            healthText.text = ((int)value).ToString();
        }
    }


    int enemiesNumber;
    float endTime;
    int score;

    void Start()
    {
        
    }

    void Update()
    {
        if(endTime - Time.time >= 0)
        {
            timerText.text = ((int)(endTime - Time.time)).ToString();
        }
        else
        {
            timerText.text = "X";
        }
    }
}
