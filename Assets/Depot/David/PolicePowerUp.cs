﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
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

    [System.Serializable]
    struct PowerUp2
    {
        public float immobilizationDuration;
        public float timeToRecharge;
        public float maxDistance;
        public bool taserOut;
        public bool hasShot;
        public LayerMask layermask;

    };

    private float timer1 = 0;
    private float timer2 = 0;

    [SerializeField]
    private PowerUp1 powerUp1;
    [SerializeField]
    private PowerUp2 powerUp2;
    
    private Player coroCop;
    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();

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
            timer1 += Time.deltaTime;

            if (timer1 > powerUp1.outlineDuration && powerUp1.isOutlined)
            {
                GameObject.Find("Player_Coronaboy").GetComponent<Outline>().OutlineWidth = 0;
                powerUp1.isOutlined = false;
            }
            if (timer1 > powerUp1.timeToRecharge)
            {
                powerUp1.abilityRecharging = false;
                timer1 = 0;
            }
        }
    }

    private void PowerUp2Manager()
    {
        if(coroCop.GetButtonDown("Ability2"))
        {
            if (!powerUp2.taserOut)
            {
                powerUp2.taserOut = true;
            }
            else
            {
                powerUp2.taserOut = false;
            }
        }

        if(coroCop.GetButtonDown("ShootGun") && powerUp2.taserOut)
        {
            powerUp2.hasShot = true;

            

            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, powerUp2.maxDistance, powerUp2.layermask))
            {
                if(hit.collider.tag == "Player")
                {
                    //hit.collider.gameObject.GetComponent<FirstPersonController>().Test();
                }
            }
        }

    }
    

}
