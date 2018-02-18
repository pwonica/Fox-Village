using UnityEngine;
using System.Collections;

public class FoxAI : MonoBehaviour
{
    public enum States
    {
        wandering,
        chase,              //anim state = 0
        NAP                 //anim state = 1
    }

    //private Rigidbody rigidbody;
    private ObjectDetection detectorFood;
    private BoxCollider boxCollider;
    private Movement movementController;
    private Fox foxCharacter;
    public MeshRenderer modelMesh;
    public ParticleSystem particleSleep;


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

    //variables for animation
    public Animator anim;
    public float animationSpeedMin;
    public float animationSpeedMax;
    private float animationSpeed;


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
        //get the movement speed from the top AI 
        moveSpeed = foxCharacter.moveSpeed;
        aiState = States.wandering;
        anim = GetComponentInChildren<Animator>();

        //calculuate the hop speed, //note, this affects ALL animations
        anim.speed = CalculateAnimationSpeed();
        

        SetNapTimer();
    }

    private float CalculateAnimationSpeed()
    {
        float scalarValue = (animationSpeedMax - animationSpeedMin) / (foxCharacter.moveSpeed_max - foxCharacter.moveSpeed_min);
        float _animationSpeed = foxCharacter.moveSpeed * scalarValue; 

        return _animationSpeed;
    }

    //todo turn the fox ai into a courotuine system
    private void Update()
    {
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
                    anim.SetInteger("whichAnimState", 0);

                    movementController.Move(currentSpeed);
                }
                break;
            case States.chase:
                //if the current way point is gone (ex: it's been eaten, then exit (find a new waypoint or wander)
                
                if (movementController.currentWaypoint == null)
                {
                    ExitChase();
                }
                
                if (movementController.ReachedLocation())
                {
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
                anim.SetInteger("whichAnimState", 1);
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
        print("Exiting nap state");
        //turn off particle system
        particleSleep.Stop();
        aiState = States.wandering;
        movementController.ResetWandering();
        napTimer = 0;
        boxCollider.enabled = true;
        detectorFood.GetComponent<BoxCollider>().enabled = true;
        foxCharacter.currentNapDecayModifier = 1;                                   //reset the modifier to the standard rate
        SetNapTimer();
    }

    public void EnterNap()
    {
        print("Entering Nap State");
        //get a random number for the nap
        aiState = States.NAP;
        particleSleep.Play();
        boxCollider.enabled = false;
        detectorFood.GetComponent<BoxCollider>().enabled = false;
        //todo change the random value to a variable if I want greater control (ex, -5, +5)
        napDuration = Random.Range(foxCharacter.averageNapTime - 5, foxCharacter.averageNapTime + 5);
        //switch the modifier to be minimize it during naps
        foxCharacter.currentNapDecayModifier = foxCharacter.napDecayModifier;


        //modelMesh.material.color = Color.blue;
        //reset the nap timer
    }
    


    public void EnterChase()
    {
        print("Entering chase");
        //assign the waypoint and then enter chase; if done the other way, will throw error b/c chase requires active waypoint
        movementController.currentWaypoint = detectorFood.targetObject;
        aiState = States.chase;
        //assign the waypoint to the object detected
    }

    public void ExitChase()
    {
        print("Exiting chase");
        movementController.ResetWandering();
        aiState = States.wandering;
        detectorFood.objectDetected = false;
    }


}
