using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerScript : UnitScript
{
    public float speed = 5.0f;
    public Joystick movementJoystick;
    public Joystick shootingJoystick;
    public Rigidbody playerRb;
    public GameObject deathScene;
    public GameObject gameScene;


    protected override void Update()
    {
        base.Update();
        uIController.Health = currentHealth;

        gameScene.SetActive(true);
        deathScene.SetActive(false);

        move();
        attack();
    }

    protected override void kill()
    {
        uIController.Health = 0;
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
        if ((shootingJoystick.Horizontal != 0 || shootingJoystick.Vertical != 0) && Time.time - lastShot > weapons.getAttackRate(weapon))
        {
            Vector3 direction = new Vector3(shootingJoystick.Horizontal, 0.0f, shootingJoystick.Vertical).normalized;
            shoot(direction);
        }
    }
}
