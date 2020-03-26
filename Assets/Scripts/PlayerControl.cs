using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerScript playerScript;
    public Rigidbody playerRb;
    public GameObject bulletPrefab;
    public Joystick movementJoystick;
    public Joystick shootingJoystick;
    public float playerSpeed = 5.0f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 maxJoystickRange;

    void Start()
    {
    }

    void FixedUpdate()
    {
        move();
        shoot();
    }

    void move()
    {
        moveDirection = new Vector3(movementJoystick.Horizontal, 0.0f, movementJoystick.Vertical) * playerSpeed;
        playerRb.velocity = moveDirection;
    }

    void shoot()
    {
        if ((shootingJoystick.Horizontal != 0 || shootingJoystick.Vertical != 0) && Time.time - playerScript.lastShot > playerScript.attackRate)
        {
            playerScript.lastShot = Time.time;
            GameObject bullet = Instantiate(bulletPrefab);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            bullet.GetComponent<BulletScript>().shooterTag = gameObject.tag;
            bullet.GetComponent<BulletScript>().damage = playerScript.damage;
            maxJoystickRange = new Vector3(shootingJoystick.Horizontal, 0.0f, shootingJoystick.Vertical).normalized;
            bullet.transform.position = transform.position + maxJoystickRange * playerScript.bulletOffset;
            bulletRb.velocity = maxJoystickRange * playerScript.bulletSpeed;
        }
    }
}
