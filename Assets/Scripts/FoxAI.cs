using UnityEngine;
using System.Collections;

public class FoxAI : MonoBehaviour
{
    public enum States
    {
        wandering,
        chase,
        NAP
    }

    //private Rigidbody rigidbody;
    private ObjectDetection detectorFood;
    private BoxCollider boxCollider;
    private Movement movementController;
    private Fox foxCharacter;
    public MeshRenderer modelMesh;

    //variables for movement
    public float moveSpeed;
    public float chaseSpeedMultiplier;                     //multiplies the base rate   
    private float chaseSpeedModifier;                      //private variable used to change rate 

    //variables for napping
    //NOTE: Variables are here and not in movement script b/c the state machine will modifiy these speeds based on what it does

    //public float napMinTime;
    //public float napMaxTime;
    private float napDuration;
    private float napTimer;
    //public float napTimeApartMin;
    //public float napTimeApartMax;
    private float napTimeApartCurrent;

    //AI State machines
    public States aiState;

    private void Awake()
    {
        foxCharacter = GetComponent<Fox>();
        modelMesh = GetComponentInChildren<MeshRenderer>();
        movementController = GetComponentInChildren<Movement>();
        detectorFood = GetComponentInChildren<ObjectDetection>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }
    private void Start()
    {
        aiState = States.wandering;
        SetNapTimer();
        
        //rigidbody = GetComponent<Rigidbody>();
    }

    //todo turn the fox ai into a courotuine system
    private void Update()
    {
        //transform.position = movementController.transform.position - transform.localPosition;
        //gameObject.transform.position = movementController.transform.position;
        //transform.position = movementController.transform.position;
        detectorFood.transform.position = movementController.transform.position;
        float currentSpeed; 
        
        switch (aiState)
        {
            //todo refactor the way I calculate movement; maybe to it's own function? Ex: SetSpeed?
            case States.wandering:
                if (movementController.ReachedLocation())
                {
                    //get a new location
                    movementController.ResetWandering();
                }
                else
                {
                    //movementController.MovingTowardsPoint();
                    //set the speed 
                    currentSpeed = moveSpeed;
                    movementController.Move(currentSpeed);
                }
                break;
            case States.chase:
                //stay stationary
                if (movementController.ReachedLocation())
                {
                    print("Reached location");

                    movementController.Move(0f);
                }
                //move at faster speed
                else
                {
                    currentSpeed = moveSpeed * chaseSpeedMultiplier;
                    movementController.Move(currentSpeed);
                }
                break;
            case States.NAP:
                movementController.Move(0f);
                Nap();
                break;

        }

    }

    private void Nap()
    {
        napTimer += Time.deltaTime;
        if (napTimer >= napDuration) { ExitNap(); }
    }

    private void SetNapTimer()
    {
        //creates a random number based on the average nap time in the fox
        napTimeApartCurrent = Random.Range(foxCharacter.averageNapApart - 10, foxCharacter.averageNapApart + 10);
        Invoke("EnterNap", napTimeApartCurrent);
    }

    private void ExitNap()
    {
        aiState = States.wandering;
        movementController.ResetWandering();
        napTimer = 0;
        boxCollider.enabled = true;
        detectorFood.GetComponent<BoxCollider>().enabled = true;
        foxCharacter.currentNapDecayModifier = 1;                                   //reset the modifier to the standard rate
        modelMesh.material.color = Color.white;
        SetNapTimer();
    }

    public void EnterNap()
    {
        //get a random number for the nap
        aiState = States.NAP;
        boxCollider.enabled = false;
        detectorFood.GetComponent<BoxCollider>().enabled = false;
        //todo change the random value to a variable if I want greater control (ex, -5, +5)
        napDuration = Random.Range(foxCharacter.averageNapTime - 5, foxCharacter.averageNapTime + 5);
        //switch the modifier to be minimize it during naps
        foxCharacter.currentNapDecayModifier = foxCharacter.napDecayModifier;
        modelMesh.material.color = Color.blue;
        //reset the nap timer
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
        movementController.ResetWandering();
        aiState = States.wandering;
        detectorFood.objectDetected = false;
    }



    /*
    private float DetermineSpeed()
    {
        float currentSpeed; 

        if (aiState == FoxAI.States.chase) {

            chaseSpeedModifier = chaseSpeedMultiplier;
        }
        else if (aiState == FoxAI.States.wandering) { chaseSpeedModifier = 1; }
        else if (aiState == FoxAI.States.NAP) { chaseSpeedModifier = 0; }                           //nap just sets it to zero

        currentSpeed =  moveSpeed * chaseSpeedModifier;


        return currentSpeed;
    }
    */





}
