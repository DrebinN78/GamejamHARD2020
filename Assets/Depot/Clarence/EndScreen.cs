using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text coronaScore;
    public Text policeScore;
    public Text results;
    float initialTimeScale;

    private void Awake()
    {
        initialTimeScale = Time.timeScale;
    }

    private void OnEnable()
    {
        Debug.Log("initial time scale = " + initialTimeScale);

        coronaScore.text = "Unmasked people: " + GameManager.unMaskedPeopleCounter;
        policeScore.text = "Masked people: " + GameManager.maskedPeopleCounter;

        if (GameManager.unMaskedPeopleCounter > GameManager.maskedPeopleCounter)
        {
            results.text = "Coroboy wins !";
        }
        else
        {
            results.text = "Corocop wins !";
        }
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = initialTimeScale;
    }
}
