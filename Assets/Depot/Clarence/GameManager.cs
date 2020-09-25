﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using Rewired;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject endScreen;

    public float timer = 60f;
    bool takingAway = false;

    public static int maskedPeopleCounter = 0;
    public static int unMaskedPeopleCounter = 0;

    public float allNPCs;

    public Text timerText;

    public Text maskedText;
    public Text unMaskedText;

    public Image maskedBar;
    public Image unmaskedBar;

    public PoliceBehaviour corocopObject;
    public CoronaBoyBehaviour coroboyObject;

    public FirstPersonController corocopClass;
    public FirstPersonController coroboyClass;

    public List<NPCBehaviour> unMaskedPeople;
    public List<NPCBehaviour> maskedPeople;

    public int p1Choice = 0;
    public int p2Choice = 0;

    private void Awake()
    {
        p1Choice = PlayerPrefs.GetInt("P1Choice");
        p2Choice = PlayerPrefs.GetInt("P2Choice");
        instance = this;
        //corocopObject = GetComponent<CoronaBoyBehaviour>();
        //coroboyObject = GetComponent<PoliceBehaviour>();
        //corocopClass = corocopObject.GetComponent<FirstPersonController>();
        //coroboyClass = coroboyObject.GetComponent<FirstPersonController>();
        AssignPlayers();
    }

    private void Start()
    {
        timerText.text = timer.ToString();
        UpdateCounter();
    }

    private void AssignPlayers()
    {
        if(p1Choice == 1)
        {
            corocopClass.rewiredPlayer = ReInput.players.GetPlayer(0);
            coroboyClass.rewiredPlayer = ReInput.players.GetPlayer(1);
        }
        else
        {
            corocopClass.rewiredPlayer = ReInput.players.GetPlayer(1);
            coroboyClass.rewiredPlayer = ReInput.players.GetPlayer(0);
        }
    }

    private void Update()
    {
        if (!takingAway && timer > 0)
            StartCoroutine(UpdateTimer());
        else if (timer <= 0)
        {
            Debug.Log("FinishedGame");
            if (!endScreen.activeSelf)
            {
                endScreen.SetActive(true);
            }
        }
    }

    public void UpdateCounter()
    {
        maskedPeopleCounter = maskedPeople.Count;
        unMaskedPeopleCounter = unMaskedPeople.Count;

        unMaskedText.text = "Unmasked people: " + unMaskedPeopleCounter;
        maskedText.text = "Masked people: " + maskedPeopleCounter;

        unmaskedBar.fillAmount = (float)unMaskedPeopleCounter / allNPCs;
        maskedBar.fillAmount = (float)maskedPeopleCounter / allNPCs;
    }

    IEnumerator UpdateTimer()
    {
        takingAway = true;

        string minutes = ((int)timer / 60).ToString("00");
        string seconds = (timer%60).ToString("00");

        timerText.text = minutes + ":" + seconds;
        yield return new WaitForSeconds(1);
        timer -= 1;
        takingAway = false;
    }

    public void RespawnCoronaBoy(CoronaBoyBehaviour player_instance)
    {
        if (unMaskedPeopleCounter == 0)
        {
            Debug.Log("Coroboy est arrêté");
            return;
        }
        else
        {
            StartCoroutine(RespawnCoroboy(player_instance));
            
        }
        
    }
    IEnumerator RespawnCoroboy(CoronaBoyBehaviour player_instance)
    {
        NPCBehaviour npcSelected = unMaskedPeople[0];
        Vector3 respawnLocation = npcSelected.transform.position;
        Vector3 NPCspawnLocation = player_instance.transform.position;
        Quaternion respawnRotation = npcSelected.transform.rotation;
        Quaternion NPCspawnRotation = player_instance.transform.rotation;
        player_instance.gameObject.SetActive(false);
        npcSelected.transform.position = NPCspawnLocation;
        npcSelected.transform.rotation = NPCspawnRotation;
        npcSelected.Mask();
        yield return new WaitForSecondsRealtime(3f);
        player_instance.transform.position = respawnLocation;
        player_instance.transform.rotation = respawnRotation;
        player_instance.gameObject.SetActive(true);
        UpdateCounter();
        yield return null;
    }
}
