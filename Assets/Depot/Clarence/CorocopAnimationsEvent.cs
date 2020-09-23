using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorocopAnimationsEvent : MonoBehaviour
{
    [SerializeField] private GameObject sparkTaser;

    public void DisableSelf()
    {
        if (gameObject != null && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
    
    public void EnableSelf()
    {
        if (gameObject != null && !gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    public void DisableSpark()
    {
        if (sparkTaser != null && sparkTaser.activeSelf)
        {
            sparkTaser.SetActive(false);
        }
    }

    public void EnebleSpark()
    {
        if (sparkTaser != null && !sparkTaser.activeSelf)
        {
            sparkTaser.SetActive(true);
        }
    }
}
