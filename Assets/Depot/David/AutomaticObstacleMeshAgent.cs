using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutomaticObstacleMeshAgent : MonoBehaviour
{
    // Put this on any obstacle that has a boxCollider

    private BoxCollider collider;
    private NavMeshObstacle obstacle;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
        obstacle = gameObject.AddComponent<NavMeshObstacle>();
        obstacle.size = collider.size;
        obstacle.size += new Vector3(0.5f, 0.5f, 5f);
    }
    
}
