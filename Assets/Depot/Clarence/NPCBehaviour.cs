using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehaviour : MonoBehaviour
{
    public bool isMasked = true;
    [SerializeField] private GameObject mask;

    // AI
    // Put the NPC on a NavMesh
    // Put a BoxCollider with "Limits" tag to keep the NPCs inside a zone
    private NavMeshAgent agent;
    private float agentSpeed;
    [SerializeField]
    private float wanderRadius;
    [SerializeField]
    private float wanderTime;
    private float timer;
    private bool isInLimits = true;
    public Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
        timer = wanderTime;
        
        if (!isMasked)
            UnMask();
        else
            Mask();
    }

    private void Update()
    {
        if (isInLimits)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTime)
            {
                Vector3 newDestination = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newDestination);
                timer = 0;
            }
        }
    }

    Vector3 RandomNavSphere(Vector3 pos, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += pos;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Limits")
        {
            Debug.Log("Exit limits");
            isInLimits = false;
            // Très très brut comme façon de le remettre dans les limites, à améliorer si possible
            StartCoroutine(PauseAgentForSeconds(3f));
            agent.SetDestination(originalPos);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Limits")
        {
            Debug.Log("Enter limits");
            isInLimits = true;
        }
    }

    IEnumerator PauseAgentForSeconds(float time)
    {
        agent.speed = 0;
        yield return new WaitForSeconds(time);
        agent.speed = agentSpeed;
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
