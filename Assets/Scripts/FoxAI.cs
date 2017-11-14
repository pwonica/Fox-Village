using UnityEngine;
using System.Collections;

public class FoxAI : MonoBehaviour
{
    public enum States
    {
        WANDER,
        CHASE,
        BEG,
        NAP
    }

    //private Rigidbody rigidbody;
    private ObjectDetection detectorFood;
    private Movement movementController;

    private bool isAlive; 
    public States aiState;

    private void Awake()
    {
        movementController = GetComponentInChildren<Movement>();
        detectorFood = GetComponentInChildren<ObjectDetection>();
    }
    private void Start()
    {
        aiState = States.WANDER;
        //activate the state machine 
    }

    IEnumerator StateMachine()
    {
        while (isAlive)
        {
            switch (aiState)
            {
                case States.WANDER:
                    movementController.MovingTowardsPoint();
                    break;
                case States.CHASE:
                    movementController.MovingTowardsPoint();
                    break;
                case States.BEG:
                    break;
                case States.NAP:
                    break;
            }
            yield return null;

        }

    }

    private void Update()
    {
        //transform.position = movementController.transform.position - transform.localPosition;
        //gameObject.transform.position = movementController.transform.position;
        //transform.position = movementController.transform.position;
        detectorFood.transform.position = movementController.transform.position;
        
        switch (aiState)
        {
            case States.WANDER:
                movementController.MovingTowardsPoint();
                break;
            case States.CHASE:
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

    

    public void EnterCHASE()
    {
        print("Entering CHASE");
        aiState = States.CHASE;
        movementController.currentWaypoint = detectorFood.targetObject;
        //assign the waypoint to the object detected
        
    }

    public void ExitCHASE()
    {
        print("Exiting CHASE");
        aiState = States.WANDER;
        movementController.ResetWANDER();
    }

    private void StateLogic()
    {
        //check for target, if target, then CHASE
        
        //else, just wander at random 
        //if exiting target, then get back to WANDER 
    }

    
    
  
 

}
