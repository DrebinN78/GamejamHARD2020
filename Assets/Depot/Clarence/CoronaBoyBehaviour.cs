using System;
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
    [Range(1f, 30f)]
    [SerializeField] private float cooldownDurationAbility1 = 1f;
    [Range(1f, 30f)]
    [SerializeField] private float cooldownDurationAbility2 = 1f;

    bool ability1ready;
    bool ability2ready;

    void Ability1()
    {
        StartCoroutine(Ability1routine());
    }

    void Ability2()
    {
        StartCoroutine(Ability2routine());
    }

    private void Update()
    {
        if (fpsController.rewiredPlayer.GetButtonDown("Ability1"))
        {
            Ability1();
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
