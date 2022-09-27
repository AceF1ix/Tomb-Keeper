using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    //Chase the player
    [SerializeField] private Transform playerPosition;
    private NavMeshAgent navMeshAgent;
    public bool hasPath;


    // Zone Control

    private Vector3 center = new Vector3(-111f, 0f, 38f);
    
    [SerializeField] private Transform enemy_pos_zone_1;
    [SerializeField] private Transform enemy_pos_zone_2;
    [SerializeField] private Transform enemy_pos_zone_3;
    [SerializeField] private Transform enemy_pos_zone_4;

    private bool zone_1_travel = true;
    private bool zone_2_travel = true;
    private bool zone_3_travel = true;
    private bool zone_4_travel = true;

    private bool in_zone_1 = false;
    private bool in_zone_2 = false;
    private bool in_zone_3 = false;
    private bool in_zone_4 = false;

    // FOV Data

    private float radius = 30;
    private float angle = 90;
    private Vector3 origin;
    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    // Patrol Data

    public Transform[] waypoints1;
    public Transform[] waypoints2;
    public Transform[] waypoints3;
    public Transform[] waypoints4;

    int wayPointIndex1;
    int wayPointIndex2;
    int wayPointIndex3;
    int wayPointIndex4;

    Vector3 target;



    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVroutine());
        UpdateDestination();
    }

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if(navMeshAgent.enabled == true && canSeePlayer) 
        {
            navMeshAgent.destination = playerPosition.position;
        }
        else if (navMeshAgent.enabled == true && !canSeePlayer)
        {
            if (Vector3.Distance(transform.position, target) < 1f)
                {
                    UpdateWayPointIndex();
                    UpdateDestination();
                }   
        }

        enemy_pos_zones(playerPosition.position);

    }

    public void New_Enemy_Pos(Vector3 position_given) {
        navMeshAgent.enabled = false;
        navMeshAgent.Warp(position_given);
        navMeshAgent.enabled = true;
    }

    public void enemy_pos_zones(Vector3 position_given) {
        
        if(position_given.x < -111f && position_given.z > 38f && zone_1_travel == true && canSeePlayer!=true) {
            New_Enemy_Pos(enemy_pos_zone_1.position);
            zone_1_travel = false;
            zone_2_travel = true;
            zone_3_travel = true;

            in_zone_1 = true;
            in_zone_2 = false;
            in_zone_3 = false;
            in_zone_4 = false;
            UpdateDestination();
        }

        if(position_given.x > -111f && position_given.z > 38f && zone_2_travel == true && canSeePlayer!=true) {
            New_Enemy_Pos(enemy_pos_zone_2.position);
            zone_2_travel = false;
            zone_1_travel = true;
            zone_4_travel = true;

            in_zone_1 = false;
            in_zone_2 = true;
            in_zone_3 = false;
            in_zone_4 = false;
            UpdateDestination();
        }

        if(position_given.x < -111f && position_given.z < 38f && zone_3_travel == true && canSeePlayer!=true) {
            New_Enemy_Pos(enemy_pos_zone_3.position);
            zone_3_travel = false;
            zone_1_travel = true;
            zone_4_travel = true;

            in_zone_1 = false;
            in_zone_2 = false;
            in_zone_3 = true;
            in_zone_4 = false;
            UpdateDestination();
        }

        if(position_given.x > -111f && position_given.z < 38f && zone_4_travel == true && canSeePlayer!=true) {
            New_Enemy_Pos(enemy_pos_zone_4.position);
            zone_4_travel = false;
            zone_2_travel = true;
            zone_3_travel = true;
            
            in_zone_1 = false;
            in_zone_2 = false;
            in_zone_3 = false;
            in_zone_4 = true;
            UpdateDestination();
        }

    }

    private IEnumerator FOVroutine()

    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()

    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2 && !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                canSeePlayer = true;
            }
            else if (distanceToTarget > radius)
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    void UpdateDestination() {
        
        if(in_zone_1 == true) {
        target = waypoints1[wayPointIndex1].position;
        navMeshAgent.destination = target;

        }

        if(in_zone_2 == true) {
        target = waypoints2[wayPointIndex2].position;
        navMeshAgent.destination = target;

        }

        if(in_zone_3 == true) {
        target = waypoints3[wayPointIndex3].position;
        navMeshAgent.destination = target;

        }

        if(in_zone_4 == true) {
        target = waypoints4[wayPointIndex4].position;
        navMeshAgent.destination = target;

        }

    }

    void UpdateWayPointIndex() {

        if(in_zone_1 == true)
        {wayPointIndex1++;}

        if(in_zone_2 == true)
        {wayPointIndex2++;}

        if(in_zone_3 == true)
        {wayPointIndex3++;}

        if(in_zone_4 == true)
        {wayPointIndex4++;}

        if(wayPointIndex1 == waypoints1.Length) {
            wayPointIndex1 = 0;
        }

        if(wayPointIndex2 == waypoints2.Length) {
            wayPointIndex2 = 0;
        }

        if(wayPointIndex3 == waypoints3.Length) {
            wayPointIndex3 = 0;
        }

        if(wayPointIndex4 == waypoints4.Length) {
            wayPointIndex4 = 0;
        }

    }


}
