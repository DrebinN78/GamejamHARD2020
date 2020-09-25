using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private Image coroboySkill1UI;
    private Image coroboySkill2UI;
    bool ability1ready = true;
    bool ability2ready = true;
    bool ability2Activated = false;
    private float timer1;
    private float timeRatio1;
    private float timer2;
    private float timeRatio2;

    [Range(1f, 1000f)]
    [SerializeField] private float timeBeforeFirstActivationAbility2 = 150f;

    private GameObject crowd;


    private void Start()
    {
        coroboySkill1UI = GameObject.Find("CoroboySkill1").GetComponent<Image>();
        coroboySkill2UI = GameObject.Find("CoroboySkill2").GetComponent<Image>();
    }

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
        if (ability2ready && ability2Activated)
        {
            StartCoroutine(Ability2routine());
        }
        else
        {
            Debug.Log("Coroboy second ability is on cooldown or deactivated");
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

        if (!ability1ready)
        {
            timer1 += Time.deltaTime;
            timeRatio1 = timer1 / (cooldownDurationAbility1 + crowDuration);
            coroboySkill1UI.fillAmount = timeRatio1;

            if(timer1 > (cooldownDurationAbility1 + crowDuration))
            {
                ability1ready = true;
            }
        }
        if (!ability2ready)
        {
            timer2 += Time.deltaTime;
            timeRatio2 = timer2 / (cooldownDurationAbility2 + rageDuration);
            coroboySkill2UI.fillAmount = timeRatio2;

            if(timer2 > (cooldownDurationAbility2 + rageDuration))
            {
                ability2ready = true;
            }
        }
        if (!ability2Activated)
        {
            timer2 += Time.deltaTime;
            timeRatio2 = timer2 / timeBeforeFirstActivationAbility2;
            coroboySkill2UI.fillAmount = timeRatio2;

            if(timer2 > timeBeforeFirstActivationAbility2)
            {
                ability2Activated = true;
            }
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
        ability1ready = false;
        crowd = Instantiate(crowPrefab, transform.position, transform.rotation);
        coroboySkill1UI.fillAmount = 0;
        timer1 = 0;
        AudioManager.instance.Play("Coroboy_UseAbility1");
        yield return new WaitForSecondsRealtime(crowDuration);
        Destroy(crowd);
        StartCoroutine(AbilityCoolDown(1));
        yield return null;
    }

    IEnumerator Ability2routine()
    {
        ability2ready = false;
        timer2 = 0;
        coroboySkill2UI.fillAmount = 0;
        AudioManager.instance.Play("Coroboy_UseAbility2");

        float originSpeed = fpsController.m_RunSpeed;
        fpsController.m_RunSpeed *= 1.5f;

        yield return new WaitForSecondsRealtime(rageDuration);
        fpsController.m_RunSpeed = originSpeed;

        StartCoroutine(AbilityCoolDown(2));
        yield return null;

        /*
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

        //retourner tous les npc de la liste "npclist" à l'état normal
        foreach (NPCBehaviour npc in npcList)
        {
            npc.enraged = false;
        }
        */
    }

    public void DestroyCurrentCrowd(float time)
    {
        Destroy(crowd, time);
    }

    IEnumerator AbilityCoolDown(int skilltocooldown)
    {
        if (skilltocooldown != 1)
        {
            yield return new WaitForSecondsRealtime(cooldownDurationAbility2);
        }
        else
        {
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
        AudioManager.instance.Play("Any_AbilityCharge");
        yield return null;

    }
}
