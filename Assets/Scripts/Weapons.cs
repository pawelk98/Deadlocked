using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int NumberOfWeapons
    {
        get { return weaponsData.Length; }
    }

    float[][] weaponsData;
    
    void Start()
    {
        weaponsData = new float[5][];

        //damage, attack rate, bullet speed, life length
        weaponsData[0] = new float[] { 1.0f, 0.2f, 0.0f, 0.1f };    //melee
        weaponsData[1] = new float[] { 2.0f, 0.5f, 30.0f, 5.0f };  //pistol
        weaponsData[2] = new float[] { 0.7f, 0.1f, 25.0f, 5.0f };  //uzi
        weaponsData[3] = new float[] { 5.0f, 1.0f, 20.0f, 5.0f };  //shotgun
        weaponsData[4] = new float[] { 3.0f, 0.2f, 40.0f, 5.0f };  //rifle
        //----------------------------------------------
    }

    void Update()
    {

    }

    public float getDamage(int weaponId) { return weaponsData[weaponId][0]; }
    public float getAttackRate(int weaponId) { return weaponsData[weaponId][1]; }
    public float getBulletSpeed(int weaponId) { return weaponsData[weaponId][2]; }
    public float getLifeLength(int weaponId) { return weaponsData[weaponId][3]; }
}
