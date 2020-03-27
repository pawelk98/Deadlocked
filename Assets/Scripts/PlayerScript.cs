using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerScript : UnitScript
{
    public float speed = 5.0f;
    public Text hpText;
    public Joystick movementJoystick;
    public Joystick shootingJoystick;
    public Rigidbody playerRb;
    public GameObject deathScene;
    public GameObject gameScene;


    protected override void Update()
    {
        base.Update();
        hpText.text = currentHealth.ToString();

        gameScene.SetActive(true);
        deathScene.SetActive(false);

        move();
        attack();
    }

    protected override void kill()
    {
        hpText.text = "0";
        base.kill();
        
        gameScene.SetActive(false);
        deathScene.SetActive(true);
    }

    void move()
    {
        Vector3 moveDirection = new Vector3(movementJoystick.Horizontal, 0.0f, movementJoystick.Vertical) * speed;
        playerRb.velocity = moveDirection;
    }

    void attack()
    {
        if ((shootingJoystick.Horizontal != 0 || shootingJoystick.Vertical != 0) && Time.time - lastShot > enemySpawner.weapons[weapon][1])
        {
            Vector3 direction = new Vector3(shootingJoystick.Horizontal, 0.0f, shootingJoystick.Vertical).normalized;
            shoot(direction);
        }
    }
}
