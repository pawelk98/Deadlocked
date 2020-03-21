using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour{
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
    private bool fired = false;
    private float lastShot = 0.0f;

    void Start() {
        playerRb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        moveDirection = new Vector3(movementJoystick.Horizontal * playerSpeed, 0.0f, movementJoystick.Vertical * playerSpeed);
        //characterController.Move(moveDirection * Time.deltaTime);
        playerRb.velocity = moveDirection;

        if(shootingJoystick.Horizontal != 0 || shootingJoystick.Vertical != 0) {
            if(Time.time - lastShot > fireRate) {
                lastShot = Time.time;
                GameObject bullet = Instantiate(bulletPrefab);
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bullet.transform.position = transform.position + new Vector3(shootingJoystick.Horizontal, 0.0f, shootingJoystick.Vertical) * bulletOffset;
                bulletRb.velocity = (bullet.transform.position - transform.position) * bulletSpeed;
                fired = true;
            }
        }
    }
}
