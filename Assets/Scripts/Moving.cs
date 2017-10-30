using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour {


    public float moveSpeed;

    private Rigidbody rigidBody;
    public bool hasTarget;

    private bool isMoving;
    private Vector3 moveDirection;
    public Transform objectTarget;


    //logic: the counters will count down to 0 to indicate movement, which is affect by other variables
    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;                            //how long it'll take to move
    private float timeToMoveCounter;

    //refactor this into a game over script
    public float waitToReload;
    private bool isReloading;
    private GameObject playerReference;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        //base the counters off the starting variable numbers and then begin a countdown
        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

    }

    // Update is called once per frame
    void Update()
    {


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
                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, 0f, Random.Range(-1f, 1f) * moveSpeed);
            }
        }


    }
}
