using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public enum BehaviorState { Wander, MoveTowardsFood }
    public BehaviorState whichState;


    public float moveSpeed;
    public float chaseSpeedModifier;
    public bool hasTarget;
    public Transform objectTarget;

    private Rigidbody rigidBody;

    private bool isMoving;
    private Vector3 moveDirection;
    
    private Vector3 waypoint;           //direction to move at all times; can take random or a target
    public float waypointChanngeMin;
    public float waypointChangeMax;
    private float waypointChangeTime;

     

    //logic: the counters will count down to 0 to indicate movement, which is affect by other variables
    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;                            //how long it'll take to move
    private float timeToMoveCounter;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        //base the counters off the starting variable numbers and then begin a countdown
        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
        //GetRandomWaypoint();

        //waypointChangeTime = Random.Range(waypointChanngeMin, waypointChangeMax);
        //InvokeRepeating("GetRandomWaypoint", 0f, waypointChangeTime);
    }

    public void GetRandomWaypoint()
    {
        waypointChangeTime = Random.Range(waypointChanngeMin, waypointChangeMax);
        waypoint = new Vector3(transform.position.x + Random.Range(-1f, 1f), 0f, transform.position.z + Random.Range(-1f, 1f));
        transform.LookAt(waypoint);
        print(waypoint + "/Change Time" + waypointChangeTime);
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        //rigidBody.velocity = transform.forward * moveSpeed;
    }

    


    void Update () {

        
        /*
        
        if (hasTarget)
        {
                //transform.LookAt(objectTarget);
                //change this to the waypoint
                transform.position = Vector3.MoveTowards(transform.position, objectTarget.transform.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.LookAt(waypoint);
            //refactor to make it less slow
            //maybe replace with move towards point? This opperates really weird 
            //rigidBody.AddRelativeForce(Vector3.forward * moveSpeed * Time.deltaTime);
            rigidBody.velocity = waypoint;
            //rigidBody.velocity = new Vector3(Random.Range(-1f, 1f) * moveSpeed, 0f, Random.Range(-1f, 1f) * moveSpeed);
        }
        */

    
        
        if (isMoving)
        {
            //begins the process of moving but stops if counter is 0
            timeToMoveCounter -= Time.deltaTime;
            rigidBody.velocity = moveDirection;
            if (timeToMoveCounter < 0f)
            {
                isMoving = false;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);

            }
        }
        //if not moving, 
        else
        {
            //subtracts time
            timeBetweenMoveCounter -= Time.deltaTime;
            //stops the velocity/movement
            rigidBody.velocity = Vector2.zero;
            //if it hits 0, will start moving, reset the counter, and pick a direction
            if (timeBetweenMoveCounter < 0f)
            {
                isMoving = true;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
                //pick a random direction to move; note doesn't change the z axis 
                //technically, you could also apply movespeed in isMoving under the velocity but it's more efficent to apply in one time rather
                //than as a constant update

                //move towards a target or towards random direction
                if (hasTarget)
                {
                    print("Moving towards target");

                    transform.LookAt(objectTarget);
                    moveDirection = Vector3.MoveTowards(transform.position, objectTarget.position, moveSpeed);
                    //moveDirection = new Vector3(objectTarget.position.x * moveSpeed, 0f, objectTarget.position.z * moveSpeed);
                    print(objectTarget.position);
                }
                else
                {
                    print("Moving nowhere");
                    moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, 0f, Random.Range(-1f, 1f) * moveSpeed);
                    print(moveDirection);
                }
            }
        }

    
    }


}
