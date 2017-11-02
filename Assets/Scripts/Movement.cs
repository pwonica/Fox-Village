using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float moveSpeed;
    public float rotationSpeed;
    private FoxAI foxAI;

    public Transform currentWaypoint;
    public Transform randomWayPoint;
    public float maxX;
    public float minX;
    public float maxZ;
    public float minZ;

    private void Start()
    {
        foxAI = GetComponentInParent<FoxAI>();
        currentWaypoint = randomWayPoint;
        GetWaypoint();
    }
    public void GetWaypoint()
    {
        currentWaypoint.position = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
    }

    public void MovingTowardsPoint()
    {
        if (currentWaypoint != null)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(waypoint.position - transform.position), rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 0f, transform.position.z), currentWaypoint.position, moveSpeed * Time.deltaTime);
            transform.LookAt(currentWaypoint);
        }

        if(foxAI.aiState == FoxAI.States.wandering)
        {
            if ((transform.position - currentWaypoint.position).magnitude < 3)
            {
                print("Arrived at point");
                GetWaypoint();
            }
        }
      
    }

    public void ResetWandering()
    {
        currentWaypoint = randomWayPoint;
        currentWaypoint.position = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));
    }

}
