using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    private NavMeshAgent navMeshAgent;
    public bool hasPath;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if(navMeshAgent.enabled == true) 
        {
            navMeshAgent.destination = playerPosition.position;
        }
    }

    public void New_Enemy_Pos(Vector3 position_given) {
        navMeshAgent.enabled = false;
        navMeshAgent.Warp(position_given);
        navMeshAgent.enabled = true;
    }

}
