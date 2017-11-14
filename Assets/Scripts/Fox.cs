using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fox : MonoBehaviour {

    private FoxAI foxAI;
    public Transform foxTransform;
    public Transform foxModel;
    private ObjectDetection detectorFood;
    private Movement movementController;


    public GameObject anchorTextReference;
    public GameObject ui_rectScrollNameReference;

    public string foxName;
    public int fullness = 70;
    public float foxDecreaseRate;
    public int fullnessDecay;

    public float hungryCheckRate;                           //how often it checks if it's hungry and deducts points
    private bool isHungry = false;



    public Text txtFoxNameDisplay;
    public GameObject uiFeedbackIcon;
    public Canvas canvas;
    public Camera cam;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        cam = FindObjectOfType<Camera>();
        foxAI = GetComponent<FoxAI>();
        foxName = NameGenerator.instance.GetName();
        Invoke("DecreaseFullness", 0f);
        //detectorFood = GetComponentInChildren<ObjectDetection>();
        //movementController = GetComponentInChildren<Movement>();
        //movementController.whichState = Movement.BehaviorState.Wander;
        //CreateFeedbackIcon("happy");
        anchorTextReference = UIManager.instance.CreateAnchoredText(foxName, foxModel);
        UIManager.instance.AddNameInRectScroll(gameObject);
    }


    public void EatFood(int valueToAdd)
    {
        //boost fullness
        fullness += valueToAdd;
        if (fullness > 100) { fullness = 100; }
        GameController.instance.AddPoints(valueToAdd);
        //create a ui icon showing a heart 
        //reset wander and follow
        foxAI.ExitCHASE();
        UIManager.instance.CreateFeedbackIcon(foxTransform, FeedbackIconType.happy);

        //movementController.objectTarget = null;
        //movementController.hasTarget = false;
    }



    private void DecreaseFullness()
    {
        fullness -= fullnessDecay;
        if (fullness < 1)
        {
            Runaway();
        }
        Invoke("DecreaseFullness", foxDecreaseRate);

    }

    private void Runaway()
    {
        //delete the UI object in the UI controller here
        UIManager.instance.RemoveNameInRectScroll(ui_rectScrollNameReference);
        //delete the UI anchor text
        anchorTextReference.SetActive(false);
        Destroy(anchorTextReference);
        GameController.instance.RemoveFox(gameObject);


        //lose points
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
    

    private void Decreasefullness()
    {
        fullness -= fullnessDecay;
        if (fullness < 1)
        {
            Runaway();
        }
        Invoke("Decreasefullness", 1f);
       
    }

 
    */


    /*
    //if the fox has left food radius, pick a new waypoint
    public void OnCollisionExitChild()
    {
        print("Exiting food detection, getting new waypoint");
        movementController.GetRandomWaypoint();
    }
    */
}
