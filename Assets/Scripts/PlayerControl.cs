using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject bulletPrefab;
    public CharacterController characterController;
    public Joystick movementJoystick;
    public Joystick shootingJoystick;
    public float playerSpeed = 5.0f;
    public float bulletOffset = 0.5f;
    public float bulletSpeed = 10.0f;
    public float fireRate = 1;

    private Rigidbody playerRb;
    private Vector3 moveDirection = Vector3.zero;
    private float lastShot = 0.0f;
    private float joystickMagnitude;
    private Vector2 maxJoystickRange;

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

            maxJoystickRange = new Vector2(shootingJoystick.Horizontal, shootingJoystick.Vertical).normalized;

            bullet.transform.position = transform.position + new Vector3(maxJoystickRange.x, 0.0f, maxJoystickRange.y) * bulletOffset;
            bulletRb.velocity = (bullet.transform.position - transform.position) * bulletSpeed;
        }
    }
}
