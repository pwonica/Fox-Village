using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fox : MonoBehaviour {


    private ObjectDetection detectorFood;
    private Movement movementController;

    private string foxName;
    public int happiness = 50;

    public int happinessDecay;



    
	// Use this for initialization
	void Start () {
        detectorFood = GetComponentInChildren<ObjectDetection>();
        movementController = GetComponentInChildren<Movement>();
        movementController.whichState = Movement.BehaviorState.Wander;

        Invoke("DecreaseHappiness", 1f);
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = movementController.transform.position;

        if (detectorFood.objectDetected)
        {
            movementController.whichState = Movement.BehaviorState.MoveTowardsFood;
            movementController.hasTarget = true;
            movementController.objectTarget = detectorFood.targetObject;
        }
        else
        {
            movementController.whichState = Movement.BehaviorState.Wander;
            movementController.hasTarget = false;
            //remove target from controller
            movementController.objectTarget = null;

        }
    }

    public void EatFood(int valueToAdd)
    {
        //boost happiness
        happiness += valueToAdd;
        //reset wander and follow
        movementController.objectTarget = null;
        detectorFood.targetObject = null;
        movementController.hasTarget = false;
    }

    private void DecreaseHappiness()
    {
        happiness -= happinessDecay;
        if (happiness < 1)
        {
            Runaway();
        }
        Invoke("DecreaseHappiness", 1f);
       
    }

 
    

    private void Runaway()
    {
        Destroy(gameObject);
        //lose points
    }
    
    /*
    //if the fox has left food radius, pick a new waypoint
    public void OnCollisionExitChild()
    {
        print("Exiting food detection, getting new waypoint");
        movementController.GetRandomWaypoint();
    }
    */
}
