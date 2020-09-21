using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timer = 60f;
    public int maskedPeopleCounter = 0;
    public int unMaskedPeopleCounter = 0;

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
        UpdateCounter();
    }

    public void UpdateCounter()
    {
        maskedPeopleCounter = maskedPeople.Count;
        unMaskedPeopleCounter = unMaskedPeople.Count;

        unMaskedText.text = "Unmasked people: " + unMaskedPeopleCounter;
        maskedText.text = "Masked people: " + maskedPeopleCounter;
    }
}
