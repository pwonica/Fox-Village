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

    public string foxName;
    public float fullness = 70;
    public float foxDecreaseRate;

    //unique fox characteristics
    public float moveSpeed;
    [HideInInspector] public float averageNapTime;
    [HideInInspector] public float averageNapApart;
    public float fullnessDecay;

    [HideInInspector] public float napDecayModifier;                          //modifier that affects hunger during a nap time (ex: 0.5, 0.75 * normal hunger decrease)     
    [HideInInspector] public float currentNapDecayModifier;                   //modifier that switches; normally is 1, but in nap state, changes to nap decay modifier

    //variables for randomizations;
    public float averageNapTime_min;
    public float averageNapTime_max;
    public float averageNapApart_min;
    public float averageNapApart_max;


    private Text txtFoxNameDisplay;
    private GameObject uiFeedbackIcon;
    //private Canvas canvas;
    //private Camera cam;

    private void Awake()
    {
        //canvas = FindObjectOfType<Canvas>();
        //cam = FindObjectOfType<Camera>();
        foxAI = GetComponent<FoxAI>();
        RandomizeFox();
    }
    private void Start()
    {
        foxName = NameGenerator.instance.GetName();
        Invoke("DecreaseFullness", 0f);
        anchorTextReference = UIManager.instance.CreateAnchoredText(foxName, foxModel);
        UIManager.instance.AddNameInRectScroll(gameObject);
    }

    private void RandomizeFox()
    {
        averageNapApart = Random.Range(averageNapApart_min, averageNapApart_max);
        averageNapTime = Random.Range(averageNapTime_min, averageNapTime_max);
    }


    public void EatFood(int valueToAdd)
    {
        GameController.instance.AddPoints(valueToAdd);
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
    }

}
