using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaBoyBehaviour : MonoBehaviour
{
    public List<Transform> objectives;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Objective")
        {
            collision.gameObject.GetComponent<Objective>().isMasked = false;
        }
    }
}
