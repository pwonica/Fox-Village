using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;
    public GameObject uiFeedbackIcon;
    public GameObject pfab_uiAnchoredText;
    public GameObject pfab_uiFoxRectText;
    private Canvas canvas;

    public Transform foxNamesHierachy;
    public Transform foxRectScroll;
    private List<GameObject> foxNameList = new List<GameObject>();



    public Text txtPoints;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        canvas = FindObjectOfType<Canvas>();

        UpdatePoints();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePoints()
    {
        txtPoints.text = GameController.instance.points.ToString();
    }

    public void AddNameInRectScroll(GameObject foxToReference)
    {
        //create the ui element
        GameObject objectToCreate = Instantiate(pfab_uiFoxRectText) as GameObject;
        objectToCreate.transform.SetParent(foxRectScroll, false);
        objectToCreate.GetComponent<FoxUIDisplay>().foxObject = foxToReference;
        //create a reference of the display text in the fox in case you want to delete it at a later point
        foxToReference.GetComponent<Fox>().ui_rectScrollNameReference = objectToCreate;
        //parent it 

        //create a reference
    }

    public void RemoveNameInRectScroll (GameObject rectScrollObject)
    {
        foxNameList.Remove(rectScrollObject);
        GameObject.Destroy(rectScrollObject);
    }

    public void CreateFeedbackIcon(Transform whichTransform, FeedbackIconType whichIconType)
    {
        GameObject objectToCreate = Instantiate(uiFeedbackIcon) as GameObject;
        RectTransform uiReference = objectToCreate.GetComponent<RectTransform>();
        objectToCreate.GetComponent<UI_floatingIcon>().whichIcon = whichIconType;

        //objectToCreate.transform.SetParent(canvas.transform, false);
        objectToCreate.GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 1);
        
        //the below is basically a lot of math that conerts the position of the transform provided to a canvas RectTransform position and then assigns it
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(whichTransform.position);
        Vector2 screenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        uiReference.anchoredPosition = screenPosition;
    }

    public GameObject CreateAnchoredText(string textToAssign, Transform transformToFollow)
    {
        //create the text object 
        GameObject objectToCreate = Instantiate(pfab_uiAnchoredText) as GameObject;
        objectToCreate.transform.SetParent(foxNamesHierachy, false);
        objectToCreate.GetComponent<Text>().text = textToAssign;
        objectToCreate.GetComponent<UIAnchor>().objectToFollow = transformToFollow;

        return objectToCreate;
    }
}
