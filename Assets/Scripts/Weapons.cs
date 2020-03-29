using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    float[][] weaponsData;
    
    void Start()
    {
        weaponsData = new float[3][];

        //damage, attack rate, bullet speed, life length
        weaponsData[0] = new float[] { 1.0f, 0.2f, 0.0f, 0.1f };    //melee
        weaponsData[1] = new float[] { 2.0f, 0.5f, 30.0f, 5.0f };  //pistol
        weaponsData[2] = new float[] { 5.0f, 1.0f, 20.0f, 5.0f };  //shotgun
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
