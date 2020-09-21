﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CoronaBoyBehaviour>() != null)
        {
            Debug.Log(name + "Collide");
            CoronaBoyBehaviour touchedCoronaBoy = other.gameObject.GetComponent<CoronaBoyBehaviour>();

            if (!touchedCoronaBoy.isPlayable && !touchedCoronaBoy.isMasked)
            {
                touchedCoronaBoy.isMasked = true;
            }
            else if (touchedCoronaBoy.isPlayable && !touchedCoronaBoy.isMasked)
            {

            }
        }
        else return;
    }
}
