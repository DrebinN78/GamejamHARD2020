using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CoronaBoyBehaviour : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private NPCBehaviour npcBehaviour;

    public bool isMasked = false;
    public bool isPlayable = true;

    private void Start()
    {
        if (isMasked && isPlayable)
            GameManager.instance.maskedPeople.Add(this);
        else if (!isMasked && !isPlayable)
            GameManager.instance.unMaskedPeople.Add(this);
        else return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayable)
            return;

        if (other.gameObject.GetComponent<CoronaBoyBehaviour>() != null)
        {
            Debug.Log(name + "Collide");
            CoronaBoyBehaviour touchedCoronaBoy = other.gameObject.GetComponent<CoronaBoyBehaviour>();

            if (!touchedCoronaBoy.isPlayable && touchedCoronaBoy.isMasked)
            {
                touchedCoronaBoy.isMasked = false;
                touchedCoronaBoy.UnMasked();
            }
        }
        else return;
    }

    //Methode pour activer le comportement NPC et désactiver le comportement FPS
    public void NPCControl()
    {
        if (fpsController == null || npcBehaviour == null)
            return;
        //desactiver les controls fps
        //activer le comportement npc
        npcBehaviour.isWandering = true;
    }

    //Methode pour activer le comportement FPS et désactiver le comportement NPC
    public void FPSControl()
    {
        if (fpsController == null || npcBehaviour == null)
            return;
        //desactiver le comportement npc
        //activer les controls fps
        npcBehaviour.isWandering = false;
    }

    public void UnMasked()
    {
        foreach (CoronaBoyBehaviour coronaBoy in GameManager.instance.maskedPeople)
        {
            if (coronaBoy == this)
                GameManager.instance.maskedPeople.Remove(coronaBoy);
        }
        GameManager.instance.unMaskedPeople.Add(this);
        GameManager.instance.UpdateCounter();
    }

    public void Masked()
    {
        foreach (CoronaBoyBehaviour coronaBoy in GameManager.instance.maskedPeople)
        {
            if (coronaBoy == this)
                GameManager.instance.unMaskedPeople.Remove(coronaBoy);
        }
        GameManager.instance.maskedPeople.Add(this);
        GameManager.instance.UpdateCounter();
    }
}
