using UnityEngine;


public class PlayerScript : UnitScript
{
    public Joystick movementJoystick;
    public Joystick shootingJoystick;
    public Rigidbody playerRb;
    public GameObject deathScene;
    public GameObject gameScene;
    public Animator animator;

    public float speedMultiplier;
    private bool isGrounded;
    private int[] ammo;
    private Vector2 moveAxis;
    private Vector2 shootAxis;
    

    protected override void Start()
    {
        base.Start();

        ammo = new int[weaponsScript.NumberOfWeapons];
        ammo[1] = -1;
        ammo[2] = 50;
        ammo[3] = 50;
        ammo[4] = 50;

        uIController.Ammo = ammo[weapon];
        uIController.Weapon = weapon;

        gameScene.SetActive(true);
        deathScene.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();
        uIController.Health = currentHealth;
        moveAxis = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
        shootAxis = new Vector2(shootingJoystick.Horizontal, shootingJoystick.Vertical);
    }

    protected override void FixedUpdate() 
    {
        base.FixedUpdate();
        move();
        attack();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag.Equals("Ammo")) 
        {
            ammo[other.GetComponent<AmmoScript>().weapon] += other.GetComponent<AmmoScript>().amount;
            Destroy(other.gameObject);

            uIController.Ammo = ammo[weapon];
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag.Equals("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.tag.Equals("Floor"))
        {
            isGrounded = false;
        }
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
        Vector3 moveDirection = new Vector3(moveAxis.x * Time.deltaTime * speedMultiplier,
                                            playerRb.velocity.y, 
                                            moveAxis.y * Time.deltaTime * speedMultiplier);

        if(!isGrounded && moveDirection.y > 0) 
        {
            moveDirection.y = 0;
        }
  
        playerRb.velocity = moveDirection;
        animator.SetFloat("isRunning", moveAxis.magnitude);

        if(shootAxis.magnitude > 0)
        {
            playerRb.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(shootAxis.x, 0, shootAxis.y));
        }
        else if(moveAxis.magnitude > 0)
        {
            playerRb.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(moveAxis.x, 0, moveAxis.y));
        }
    }

    void attack()
    {
        if(shootAxis.magnitude > 0)
        {
            if(Time.time - lastShot > weaponsScript.getAttackRate(weapon))
            {
                Vector3 direction = new Vector3(shootingJoystick.Horizontal, 0.0f, shootingJoystick.Vertical).normalized;
                shoot(direction);
            }
        }
    }

    protected override void shoot(Vector3 direction)
    {

        base.shoot(direction);

        if (weapon != 1)
        {
            ammo[weapon]--;
            uIController.Ammo = ammo[weapon];
        }

        if (ammo[weapon] == 0)
        {
            switchWeapon();
        }
    }

    public void switchWeapon()
    {
        do 
        {
            weapon = (weapon + 1) % weaponsScript.NumberOfWeapons;
        } while (ammo[weapon] == 0);

        uIController.Ammo = ammo[weapon];
        uIController.Weapon = weapon;
    }

    public void pickUpAmmo(int weaponId, int amount)
    {
        ammo[weaponId] += amount;
    }
}
