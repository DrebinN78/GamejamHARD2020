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
    [Range(1f,5f)]
    [SerializeField] private float crowDuration;

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
        GameObject crowd = Instantiate(crowPrefab);
        yield return new WaitForSecondsRealtime(crowDuration);
        Destroy(crowd);
        yield return null;
    }

    IEnumerator Ability2routine()
    {
        yield return null;
    }
}
