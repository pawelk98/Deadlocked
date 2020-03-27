using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : UnitScript
{
    public float speed = 5.0f;
    public Text hpText;
    public Joystick movementJoystick;
    public Joystick shootingJoystick;
    public Rigidbody playerRb;


    protected override void Update()
    {
        base.Update();
        hpText.text = "HP " + base.currentHealth.ToString();

        move();
        attack();
    }

    protected override void kill()
    {
        hpText.text = "HP  0";
        base.kill();
    }

    void move()
    {
        Vector3 moveDirection = new Vector3(movementJoystick.Horizontal, 0.0f, movementJoystick.Vertical) * speed;
        playerRb.velocity = moveDirection;
    }

    void attack()
    {
        if ((shootingJoystick.Horizontal != 0 || shootingJoystick.Vertical != 0) && Time.time - lastShot > weapons[weapon][1])
        {
            Vector3 direction = new Vector3(shootingJoystick.Horizontal, 0.0f, shootingJoystick.Vertical).normalized;
            shoot(direction);
        }
    }
}
