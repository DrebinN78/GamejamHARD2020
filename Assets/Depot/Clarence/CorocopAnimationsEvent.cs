using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorocopAnimationsEvent : MonoBehaviour
{
    [SerializeField] private GameObject sparkTaser;
    [SerializeField] private Transform sparkInitialPosition;

    public void DisableSelf()
    {
        if (gameObject != null && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
