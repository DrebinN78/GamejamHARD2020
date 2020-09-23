using System.Collections;
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

    public float allNPCs = 5;

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
            corocopClass.rewiredPlayer = ReInput.players.GetPlayer(1);
            coroboyClass.rewiredPlayer = ReInput.players.GetPlayer(0);
        }
        else
        {
            corocopClass.rewiredPlayer = ReInput.players.GetPlayer(0);
            coroboyClass.rewiredPlayer = ReInput.players.GetPlayer(1);
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
        yield return new WaitForSeconds(1);
        timer -= 1;
        timerText.text = timer.ToString();
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
            int npcSelector = Random.Range(0, unMaskedPeopleCounter);
            NPCBehaviour npcSelected = unMaskedPeople[npcSelector];
            Vector3 respawnLocation = unMaskedPeople[npcSelector].transform.position;
            Vector3 NPCspawnLocation = player_instance.transform.position;
            player_instance.transform.position = respawnLocation;
            npcSelected.transform.position = NPCspawnLocation;
            npcSelected.isMasked = true;
        }
        
    }
}
