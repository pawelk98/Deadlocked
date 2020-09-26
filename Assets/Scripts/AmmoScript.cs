using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    public Transform ammo;
    public int weapon = 1;
    public int amount = 10;


    void Update() 
    {
        ammo.Rotate(0,1,0);
    }
}
