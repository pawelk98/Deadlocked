using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject[] bulletPrefabs;

    public int NumberOfWeapons
    {
        get { return weaponsData.Length; }
    }

    float[][] weaponsData;
    
    void Start()
    {
        weaponsData = new float[5][];

        //damage, attack rate, bullet speed, life length, recoil
        weaponsData[0] = new float[] { 1.0f, 0.2f, 0.0f, 0.1f, 0f };   //melee
        weaponsData[1] = new float[] { 2.0f, 0.5f, 40.0f, 4.0f, 2.0f };  //pistol
        weaponsData[2] = new float[] { 0.7f, 0.1f, 35.0f, 4.0f, 5.0f };  //uzi
        weaponsData[3] = new float[] { 3.0f, 0.2f, 50.0f, 4.0f, 1.0f };  //rifle
        weaponsData[4] = new float[] { 5.0f, 1.0f, 30.0f, 4.0f, 5.0f };  //shotgun
        //----------------------------------------------
    }

    void Update()
    {

    }

    public float getDamage(int weaponId) { return weaponsData[weaponId][0]; }
    public float getAttackRate(int weaponId) { return weaponsData[weaponId][1]; }
    public float getBulletSpeed(int weaponId) { return weaponsData[weaponId][2]; }
    public float getLifeLength(int weaponId) { return weaponsData[weaponId][3]; }
    public float getRecoil(int weaponId) { return weaponsData[weaponId][4]; }
}
