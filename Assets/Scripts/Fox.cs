using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fox : MonoBehaviour {

    private FoxAI foxAI;
    private ObjectDetection detectorFood;
    private Movement movementController;

    private string foxName;
    public int happiness = 50;

    public int happinessDecay;

    private void Start()
    {
        foxAI = GetComponent<FoxAI>();
        Invoke("DecreaseHappiness", 1f);
        //detectorFood = GetComponentInChildren<ObjectDetection>();
        //movementController = GetComponentInChildren<Movement>();
        //movementController.whichState = Movement.BehaviorState.Wander;

    }
    public void EatFood(int valueToAdd)
    {
        //boost happiness
        happiness += valueToAdd;
        GameController.instance.AddPoints(valueToAdd);
        //create a ui icon showing a heart 
        //reset wander and follow
        foxAI.ExitChase();
       
        //movementController.objectTarget = null;
        //movementController.hasTarget = false;
    }
    /*
   
	
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
    

    private void DecreaseHappiness()
    {
        happiness -= happinessDecay;
        if (happiness < 1)
        {
            Runaway();
        }
        Invoke("DecreaseHappiness", 1f);
       
    }

 
    */

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
