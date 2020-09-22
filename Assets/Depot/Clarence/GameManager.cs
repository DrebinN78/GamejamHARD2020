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

    public float allNPCs = 5;

    public Text timerText;

    public Text maskedText;
    public Text unMaskedText;

    public Image maskedBar;
    public Image unmaskedBar;

    public List<NPCBehaviour> unMaskedPeople;
    public List<NPCBehaviour> maskedPeople;

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

    public void RespawnCoronaBoy(NPCBehaviour player_instance)
    {
        int npcSelector = Random.Range(0, unMaskedPeopleCounter);
        NPCBehaviour npcSelected = unMaskedPeople[npcSelector];
        Transform respawnLocation = unMaskedPeople[npcSelector].transform;
        Transform NPCspawnLocation = player_instance.transform;
        player_instance.transform.Translate(respawnLocation.position);
        npcSelected.transform.Translate(NPCspawnLocation.position);
        npcSelected.isMasked = true;
    }
}
