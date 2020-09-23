using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public bool isMasked = true;
    [SerializeField] private GameObject mask;

    private void Start()
    {
        if (!isMasked)
            UnMask();
        else
            Mask();
    }

    public void UnMask()
    {
        GameManager.instance.unMaskedPeople.Add(this);
        mask.SetActive(false);
        foreach (NPCBehaviour coronaBoy in GameManager.instance.maskedPeople)
        {
            if (coronaBoy == this)
            {
                GameManager.instance.maskedPeople.Remove(coronaBoy);
                GameManager.instance.UpdateCounter();
                return;
            }
        }
    }

    public void Mask()
    {
        GameManager.instance.maskedPeople.Add(this);
        mask.SetActive(true);
        foreach (NPCBehaviour coronaBoy in GameManager.instance.unMaskedPeople)
        {
            if (coronaBoy == this)
            {
                GameManager.instance.unMaskedPeople.Remove(coronaBoy);
                GameManager.instance.UpdateCounter();
                return;
            }
        }
    }
}
