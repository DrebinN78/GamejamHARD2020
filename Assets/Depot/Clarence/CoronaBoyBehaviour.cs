﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CoronaBoyBehaviour : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private NPCBehaviour npcBehaviour;
    [SerializeField] private GameObject crowPrefab;
    [Range(1f, 5f)]
    [SerializeField] private float crowDuration = 1f;
    [Range(1f, 5f)]
    [SerializeField] private float rageDuration = 1f;
    [Range(1f, 30f)]
    [SerializeField] private float cooldownDurationAbility1 = 1f;
    [Range(1f, 30f)]
    [SerializeField] private float cooldownDurationAbility2 = 1f;
    [Range(1f, 1000f)]
    [SerializeField] private float range = 250f;
    

    bool ability1ready = true;
    bool ability2ready = true;

    void Ability1()
    {
        if (ability1ready)
        {
            StartCoroutine(Ability1routine());
        }
        else
        {
            Debug.Log("Coroboy first ability is on cooldown");
        }
        
    }

    void Ability2()
    {
        if (ability2ready)
        {
            StartCoroutine(Ability2routine());
        }
        else
        {
            Debug.Log("Coroboy second ability is on cooldown");
        }
    }

    private void Update()
    {
        if(fpsController.rewiredPlayer.GetButtonDown("Ability1"))
        {
            Ability1();
        }
        if(fpsController.rewiredPlayer.GetButtonDown("Ability2"))
        {
            Ability2();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<NPCBehaviour>() != null)
        {
            Debug.Log(name + "Collide");
            NPCBehaviour touchedNPC = other.gameObject.GetComponent<NPCBehaviour>();

            if (touchedNPC.isMasked)
            {
                touchedNPC.UnMask();
                touchedNPC.isMasked = false;
            }
        }
        else return;
    }

    bool CheckDistance(Vector3 origin, Vector3 target, float maxRange)
    {
        Vector3 distance = new Vector3(target.x - origin.x, target.y - origin.y, target.z - origin.z);
        float sqrLen = distance.sqrMagnitude;
        if(sqrLen< maxRange*maxRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator Ability1routine()
    {
        GameObject crowd = Instantiate(crowPrefab, transform.position, transform.rotation);
        yield return new WaitForSecondsRealtime(crowDuration);
        Destroy(crowd);
        StartCoroutine(AbilityCoolDown(1));
        yield return null;
    }

    IEnumerator Ability2routine()
    {
        //List comprenant tout les NPC a convertir
        List<NPCBehaviour> npcList = new List<NPCBehaviour>();
        foreach(NPCBehaviour npc in GameManager.instance.maskedPeople)
        {
            npcList.Add(npc);
        }
        foreach (NPCBehaviour npc in GameManager.instance.unMaskedPeople)
        {
            npcList.Add(npc);
        }
        List<NPCBehaviour> npcToIgnore = new List<NPCBehaviour>();
        foreach (NPCBehaviour pos in npcList)
        {
            
            if (!CheckDistance(transform.position, pos.transform.position, range))
            {
                npcToIgnore.Add(pos);
            }
            else 
            {
                Debug.Log("Coronaboy converted" + gameObject.name);
            }
        }
        foreach(NPCBehaviour npc in npcToIgnore)
        {
            npcList.Remove(npc);
        }
        npcToIgnore = null;
        //faire foncer tous les npcs de la liste "npclist" sur le corocop
        foreach (NPCBehaviour npc in npcList)
        {
            npc.enraged = true;
        }


        yield return new WaitForSecondsRealtime(rageDuration);

        //retourner tous les npc de la liste "npclist" à l'état normal
        foreach (NPCBehaviour npc in npcList)
        {
            npc.enraged = false;
        }

        StartCoroutine(AbilityCoolDown(2));
        yield return null;
    }

    IEnumerator AbilityCoolDown(int skilltocooldown)
    {
        if (skilltocooldown != 1)
        {
            ability2ready = false;
            yield return new WaitForSecondsRealtime(cooldownDurationAbility2);
        }
        else
        {
            ability1ready = false;
            yield return new WaitForSecondsRealtime(cooldownDurationAbility1);
        }
        if (skilltocooldown != 1)
        {
            ability2ready = true;
        }
        else
        {
            ability1ready = true;
        }
        yield return null;

    }
}
