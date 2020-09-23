using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PolicePowerUp : MonoBehaviour
{
    [System.Serializable]
    struct PowerUp1
    {
        public float outlineDuration;
        public float timeToRecharge;
        public bool abilityRecharging;
        public bool isOutlined;
    
    };

    private float timer = 0;

    [System.Serializable]
    struct PowerUp2
    {
        public float timeToRecharge;
    };

    [SerializeField]
    private PowerUp1 powerUp1;
    // [SerializeField]
    private PowerUp2 powerUp2;
    
    private Player coroCop;

    private void Start()
    {
        if(GameManager.instance.p1Choice == 1)
        {
            coroCop = ReInput.players.GetPlayer(1);
        }
        else
        {
            coroCop = ReInput.players.GetPlayer(0);
        }
    }

    private void Update()
    {
        PowerUp1Manager();

        PowerUp2Manager();
    }

    private void PowerUp1Manager()
    {
        if (coroCop.GetButtonDown("Ability1") && !powerUp1.abilityRecharging)
        {
            powerUp1.abilityRecharging = true;
            powerUp1.isOutlined = true;
            GameObject.Find("Player_Coronaboy").GetComponent<Outline>().OutlineWidth = 10;
        }

        if (powerUp1.abilityRecharging)
        {
            timer += Time.deltaTime;

            if (timer > powerUp1.outlineDuration && powerUp1.isOutlined)
            {
                GameObject.Find("Player_Coronaboy").GetComponent<Outline>().OutlineWidth = 0;
                powerUp1.isOutlined = false;
            }
            if (timer > powerUp1.timeToRecharge)
            {
                powerUp1.abilityRecharging = false;
                timer = 0;
            }
        }
    }

    private void PowerUp2Manager()
    {

    }
    

}
