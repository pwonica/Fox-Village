using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {

    private FoxAI foxAI;
    public NavMeshAgent navMesh; 

    //variables for the waypoint system
    public Transform currentWaypoint;
    public Transform randomWayPoint;
    Vector3 targetPosition;
    public float maxX;
    public float minX;
    public float maxZ;
    public float minZ;

    //variables for transform
    private float originalYPos;
    private float originalZRotation;
    public float nappingYPos;


    private void Awake()
    {
        foxAI = GetComponentInParent<FoxAI>();
        navMesh = GetComponent<NavMeshAgent>();

    }
    private void Start()
    {
        currentWaypoint = randomWayPoint;
        GetRandomWaypoint();
    }

    public void GetRandomWaypoint()
    {
        randomWayPoint.position = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
    }

    public void Move(float moveSpeed)
    {

        /*
        //todo refactor the move code at some point, try and get all the conditions in the Fox AI and hae this just move it, maybe have a function: Move(movespeed, bool rotate?)
        if (foxAI.aiState == FoxAI.States.chase) { chaseSpeedModifier = chaseSpeedMultiplier; }
        else if (foxAI.aiState == FoxAI.States.wandering) { chaseSpeedModifier = 1; }
        else if (foxAI.aiState == FoxAI.States.NAP) { chaseSpeedModifier = 0; }
        */
        if (foxAI.aiState == FoxAI.States.chase && ReachedLocation())
        {
            navMesh.speed = moveSpeed;
            navMesh.updateRotation = false;
        }
        else
        {
            navMesh.speed = moveSpeed;
            navMesh.updateRotation = true;
            navMesh.SetDestination(currentWaypoint.transform.position);
        }

    }

    public bool ReachedLocation()
    {
        //sets everything to 0 location to ensure that the distance is caluaculated on two axis rather than 3 (which leads to inaccuratacies) 
        Vector3 currentVector = transform.position; currentVector.y = 0;
        Vector3 targetVector = currentWaypoint.position; targetVector.y = 0;
        return Vector3.Distance(currentVector, targetVector) < 0.5f;
    }

    public void ResetWandering()
    {
        print("Resetting waypoint");
        currentWaypoint = randomWayPoint;
        GetRandomWaypoint();
    }

}
