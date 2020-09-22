using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CoronaBoyBehaviour : MonoBehaviour
{
    [SerializeField] private FirstPersonController fpsController;
    [SerializeField] private NPCBehaviour npcBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NPCBehaviour>() != null)
        {
            Debug.Log(name + "Collide");
            NPCBehaviour touchedNPC = other.gameObject.GetComponent<NPCBehaviour>();

            if (touchedNPC.isMasked)
            {
                touchedNPC.UnMask();
                touchedNPC.isMasked = false;
            }
        }
        else return;
    }
}
