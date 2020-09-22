using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timer = 60f;
    bool takingAway = false;

    public static int maskedPeopleCounter = 0;
    public static int unMaskedPeopleCounter = 0;

    public Text timerText;
    public Text maskedText;
    public Text unMaskedText;

    public List<CoronaBoyBehaviour> unMaskedPeople;
    public List<CoronaBoyBehaviour> maskedPeople;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timerText.text = timer.ToString();
        UpdateCounter();
    }

    private void Update()
    {
        if (!takingAway && timer > 0)
            StartCoroutine(UpdateTimer());
    }

    public void UpdateCounter()
    {
        maskedPeopleCounter = maskedPeople.Count;
        unMaskedPeopleCounter = unMaskedPeople.Count;

        unMaskedText.text = "Unmasked people: " + unMaskedPeopleCounter;
        maskedText.text = "Masked people: " + maskedPeopleCounter;
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
        int npcSelector = Random.Range(0, unMaskedPeopleCounter);
        CoronaBoyBehaviour npcSelected = unMaskedPeople[npcSelector];
        Transform respawnLocation = unMaskedPeople[npcSelector].transform;
        Transform NPCspawnLocation = player_instance.transform;
        player_instance.transform.Translate(respawnLocation.position);
        npcSelected.transform.Translate(NPCspawnLocation.position);
        npcSelected.isMasked = true;
    }
}
