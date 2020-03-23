using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Joystick movementJoystick;
    public Joystick shootingJoystick;
    public float playerSpeed = 5.0f;
    public float bulletOffset = 1.0f;
    public float bulletSpeed = 10.0f;
    public int bulletDamage = 10;
    public float fireRate = 1;

    private Rigidbody playerRb;
    private Vector3 moveDirection = Vector3.zero;
    private float lastShot = 0.0f;
    private float joystickMagnitude;
    private Vector3 maxJoystickRange;

    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        moveDirection = new Vector3(movementJoystick.Horizontal, 0.0f, movementJoystick.Vertical) * playerSpeed;
        playerRb.velocity = moveDirection;

        if ((shootingJoystick.Horizontal != 0 || shootingJoystick.Vertical != 0) && Time.time - lastShot > fireRate)
        {
            lastShot = Time.time;
            GameObject bullet = Instantiate(bulletPrefab);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            bullet.GetComponent<BulletScript>().shooterTag = gameObject.tag;
            bullet.GetComponent<BulletScript>().damage = bulletDamage;
            maxJoystickRange = new Vector3(shootingJoystick.Horizontal, 0.0f, shootingJoystick.Vertical).normalized;
            bullet.transform.position = transform.position + maxJoystickRange * bulletOffset;
            bulletRb.velocity = maxJoystickRange * bulletSpeed;
        }
    }
}
