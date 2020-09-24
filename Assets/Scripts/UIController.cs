using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerScript playerScript;
    public Text timerText;
    public Text enemiesNumberText;
    public Text scoreText;
    public Text finishScoreText;
    public Text healthText;
    public Text ammoText;
    public Text weaponNameText;
    public Button switchWeaponBtn;

    public int EnemiesNumber
    {
        get { return enemiesNumber; }
        set
        {
            enemiesNumber = value;
            enemiesNumberText.text = value.ToString();
        }
    }

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = value.ToString();
            finishScoreText.text = value.ToString();
        }
    }

    public float Health
    {
        set
        {
            healthText.text = ((int)value).ToString();
        }
    }

    public int Ammo
    {
        set
        {
            if (value != -1)
            {
                ammoText.text = value.ToString();
            }
            else
            {
                ammoText.text = "∞";
            }
        }
    }

    public int Weapon
    {
        set
        {
            string name;
            switch (value)
            {
                case 1:
                    name = "Pistol";
                    break;

                case 2:
                    name = "Uzi";
                    break;

                case 3:
                    name = "Shotgun";
                    break;

                case 4:
                    name = "Rifle";
                    break;

                default:
                    name = "Unknown";
                    break;
            }

            weaponNameText.text = name;
        }
    }


    int enemiesNumber;
    float startTime;
    int score;

    void Start()
    {
        startTime = Time.time;
        switchWeaponBtn.onClick.AddListener(switchWeapon);
    }

    void Update()
    {
        int seconds = (int)(Time.time - startTime);
        timerText.text = (seconds / 60).ToString() + ":" + (seconds % 60).ToString();
    }

    void switchWeapon()
    {
        if (playerScript != null)
        {
            playerScript.switchWeapon();
        }
    }
}
