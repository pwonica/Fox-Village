using UnityEngine;
using System.Collections;

public class FoxAI : MonoBehaviour
{
    public enum States
    {
        wandering,
        chase,
        begging
    }

    private Rigidbody rigidbody;
    private ObjectDetection detectorFood;
    private Movement movementController;


    public States aiState;


    private void Start()
    {
        aiState = States.wandering;
        rigidbody = GetComponent<Rigidbody>();
        movementController = GetComponentInChildren<Movement>();
        detectorFood = GetComponentInChildren<ObjectDetection>();

        
    }

    private void Update()
    {
        //transform.position = movementController.transform.position - transform.localPosition;
        //gameObject.transform.position = movementController.transform.position;
        //transform.position = movementController.transform.position;
        detectorFood.transform.position = movementController.transform.position;


        if (detectorFood.objectDetected)
        {
            //movementController.whichState = Movement.BehaviorState.MoveTowardsFood;
            //movementController.hasTarget = true;
            //movementController.objectTarget = detectorFood.targetObject;
        }
        else
        {
            //movementController.whichState = Movement.BehaviorState.Wander;
            //movementController.hasTarget = false;
            //remove target from controller
            //movementController.objectTarget = null;

        }

        switch (aiState)
        {
            case States.wandering:
                movementController.MovingTowardsPoint();
                break;
            case States.chase:
                movementController.MovingTowardsPoint();
                break;

        }
        /*
        //check to see if at location
        if ((transform.position - wayPoint).sqrMagnitude < 9)
        {
            Wander();
        }
        */

    }

    public void EnterChase()
    {
        print("Entering chase");
        aiState = States.chase;
        movementController.currentWaypoint = detectorFood.targetObject;
        //assign the waypoint to the object detected
        
    }

    public void ExitChase()
    {
        print("Exiting chase");
        aiState = States.wandering;
        movementController.ResetWandering();
    }

    private void StateLogic()
    {
        //check for target, if target, then chase
        
        //else, just wander at random 
        //if exiting target, then get back to wandering 
    }

    

    //go after food!
    private void Chase()
    {

    }
    

  
 

}
