using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : UnitScript
{
    public Text hpText;

    public override void Update()
    {
        hpText.text = "HP " + base.currentHealth.ToString();
    }

    public override void kill()
    {
        hpText.text = "HP  0";
        base.kill();
    }
}
