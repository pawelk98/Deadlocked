using UnityEngine;


public class PlayerScript : UnitScript
{
    public Joystick movementJoystick;
    public Joystick shootingJoystick;
    public Rigidbody playerRb;
    public GameObject deathScene;
    public GameObject gameScene;

    public float speed = 5.0f;

    private int[] ammo;

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

        move();
        attack();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("Ammo")) {
            ammo[other.GetComponent<AmmoScript>().weapon] += other.GetComponent<AmmoScript>().amount;
            Destroy(other.gameObject);

            uIController.Ammo = ammo[weapon];
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
        Vector3 moveDirection = new Vector3(movementJoystick.Horizontal, 0.0f, movementJoystick.Vertical) * speed;
        playerRb.velocity = moveDirection;
    }

    void attack()
    {
        if ((shootingJoystick.Horizontal != 0 || shootingJoystick.Vertical != 0) && Time.time - lastShot > weaponsScript.getAttackRate(weapon))
        {
            Vector3 direction = new Vector3(shootingJoystick.Horizontal, 0.0f, shootingJoystick.Vertical).normalized;
            shoot(direction);
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
