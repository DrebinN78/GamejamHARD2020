using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timer = 60f;
    public List<CoronaBoyBehaviour> unMaskedPeople;
    public List<CoronaBoyBehaviour> maskedPeople;

    private void Awake()
    {
        instance = this;
    }

    
}
