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
            NPCBehaviour touchedCoronaBoy = other.gameObject.GetComponent<NPCBehaviour>();

            if (!touchedCoronaBoy.isMasked)
            {
                touchedCoronaBoy.Mask();
                touchedCoronaBoy.isMasked = true;
            }
            else if (!touchedCoronaBoy.isMasked)
            {
                GameManager.instance.RespawnCoronaBoy(touchedCoronaBoy);
            }
        }
        else return;
    }
}
