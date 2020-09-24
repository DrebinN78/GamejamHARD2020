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
    private bool goBackToOrigin;
    public Vector3 originalPos;

    //Animation
    private Animator anim;
    [SerializeField]
    private bool isMoving;

    public bool enraged;

    private void Start()
    {
        originalPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agentSpeed = agent.speed;
        timer = wanderTime;

        if (!isMasked)
        {
            Debug.Log("is not masked");
            UnMask();
        }
        else
        {
            Debug.Log("is masked");
            Mask();
        }
    }

    private void Update()
    {
        if (isInLimits && !enraged)
        {
            if (agent.speed == 0f)
            {
                agent.speed = agentSpeed;
            }

            timer += Time.deltaTime;

            if (timer >= wanderTime)
            {
                Vector3 newDestination = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newDestination);
                timer = 0;
            }
        }

        if (enraged)
        {
            agent.SetDestination(GameObject.Find("Player_Police").transform.position);
            if(agent.remainingDistance < 2f)
            {
                agent.speed = 0;
            }
            else
            {
                agent.speed = agentSpeed;
            }
        }

        if (!isInLimits && !goBackToOrigin && !enraged)
        {
            if (agent.speed == 0f)
            {
                agent.speed = agentSpeed;
            }

            goBackToOrigin = true;
            // Très très brut comme façon de le remettre dans les limites, à améliorer si possible
            StartCoroutine(PauseAgentForSeconds(0.5f));
            agent.SetDestination(originalPos);
        }

        if (agent.velocity != Vector3.zero && !isMoving)
        {
            isMoving = true;
            anim.SetBool("IsWalk", true);
        }
        else if(agent.velocity == Vector3.zero)
        {
            anim.SetBool("IsWalk", false);
            isMoving = false;
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
        if (other.gameObject.tag == "Limits")
        {
            Debug.Log("Exit limits");
            isInLimits = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Limits")
        {
            Debug.Log("Enter limits");
            goBackToOrigin = false;
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
                Debug.Log("blablou");
                GameManager.instance.maskedPeople.Remove(coronaBoy);
                break;
            }
        }
        GameManager.instance.UpdateCounter();
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
                break;
            }
        }
        GameManager.instance.UpdateCounter();
    }
    
}
