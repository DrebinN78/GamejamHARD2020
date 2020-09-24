using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NPCBehaviour>() != null)
        {
            Debug.Log(name + "Collide");
            NPCBehaviour touchedNPC = other.gameObject.GetComponent<NPCBehaviour>();

            if (!touchedNPC.isMasked)
            {
                touchedNPC.Mask();
                touchedNPC.isMasked = true;
            }
        }
        else if (other.gameObject.GetComponent<CoronaBoyBehaviour>() != null) 
        {
            other.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().Unstun();
            GameManager.instance.RespawnCoronaBoy(GameManager.instance.coroboyObject);
        }
        else return;
    }
}
