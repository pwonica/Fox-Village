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

    [HideInInspector] public GameObject anchorTextReference;
    [HideInInspector] public GameObject ui_rectScrollNameReference;

    //FOXDATA Variables
    public string foxName;
    public float fullness = 70;
    public float moveSpeed;
    [HideInInspector] public float averageNapTime;
    public float fullnessDecay;


    //Non-flexbile unique variables 
    public float fullnessDecreaseFrequency;                                     //how often the fox will decrease it's fullness
    [HideInInspector] public float averageNapApart;
    [HideInInspector] public float napDecayModifier;                          //modifier that affects hunger during a nap time (ex: 0.5, 0.75 * normal hunger decrease)     
    [HideInInspector] public float currentNapDecayModifier;                   //modifier that switches; normally is 1, but in nap state, changes to nap decay modifier

    //FLEXIBLE variables 
    public float averageNapTime_min;
    public float averageNapTime_max;
    public float averageFullnessDecay_min;
    public float averageFullnessDecay_max;
    public float moveSpeed_min;
    public float moveSpeed_max;

    //STATIC + random
    public float averageNapApart_min;
    public float averageNapApart_max;


    private Text txtFoxNameDisplay;
    private GameObject uiFeedbackIcon;

    private void Awake()
    {
        //canvas = FindObjectOfType<Canvas>();
        //cam = FindObjectOfType<Camera>();
        foxAI = GetComponent<FoxAI>();
    }
    private void Start()
    {
        //foxName = NameGenerator.instance.GetName();
        Invoke("DecreaseFullness", 0f);
        anchorTextReference = UIManager.instance.CreateAnchoredText(foxName, foxModel);
        UIManager.instance.AddNameInRectScroll(gameObject);

    }


    public void RandomizeFox()
    {
        averageNapApart = Random.Range(averageNapApart_min, averageNapApart_max);
        averageNapTime = Random.Range(averageNapTime_min, averageNapTime_max);
        fullnessDecay = Random.Range(averageFullnessDecay_min, averageFullnessDecay_max);
        moveSpeed = Random.Range(moveSpeed_min, moveSpeed_max);
        foxName = NameGenerator.instance.GetName();
        name = foxName;
    }
    
    public void EatFood(int valueToAdd)
    {
        UIManager.instance.CreateFeedbackIcon(foxTransform, FeedbackIconType.happy);

        foxAI.ExitChase();
        fullness += valueToAdd;
        if (fullness > 100){ fullness = 100; }
        //movementController.objectTarget = null;
        //movementController.hasTarget = false;
    }



    private void DecreaseFullness()
    {
        fullness -= fullnessDecay * currentNapDecayModifier;
        if (fullness < 1)
        {
            Runaway();
        }
        Invoke("DecreaseFullness", fullnessDecreaseFrequency);

    }
    
    private void Runaway()
    {
        //delete the UI object in the UI controller here
        UIManager.instance.RemoveNameInRectScroll(ui_rectScrollNameReference);
        //delete the UI anchor text
        anchorTextReference.SetActive(false);
        Destroy(anchorTextReference);
        GameController.instance.RemoveFox(gameObject);
    }

}
