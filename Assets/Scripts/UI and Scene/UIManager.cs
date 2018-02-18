using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIManager : MonoBehaviour {

    public static UIManager instance = null;
    public GameObject uiFeedbackIcon;
    public GameObject pfab_uiAnchoredText;
    public GameObject pfab_uiFloatingPopup;
    public GameObject pfab_uiFoxInfoRect;
    public GameObject pfab_uiFoxInfoPopup;
    private Canvas canvas;

    private bool isPopupActive = false;

    public Transform foxNamesHierachy;
    public Transform foxRectScroll;
    public Slider uiPassivePointSlider;
    private List<GameObject> foxNameList = new List<GameObject>();

    public Text txtPoints;

    //activate the static instance of this object
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
        canvas = FindObjectOfType<Canvas>();
    }

    // Use this for initialization
    void Start () {
        UpdatePoints();
        uiPassivePointSlider.maxValue = GameController.instance.passivePointBoost_time;
    }

    private void Update()
    {
        uiPassivePointSlider.value = GameController.instance.passiveBoostTimer;
    }

    public void UpdatePoints()
    {
        txtPoints.text = GameController.instance.points.ToString();
    }

    public void AddNameInRectScroll(GameObject foxToReference)
    {
        //create the ui element
        GameObject objectToCreate = Instantiate(pfab_uiFoxInfoRect) as GameObject;
        objectToCreate.transform.SetParent(foxRectScroll, false);
        objectToCreate.GetComponent<UI_foxInfoRect>().foxObject = foxToReference;
        //create a reference of the display text in the fox in case you want to delete it at a later point
        foxToReference.GetComponent<Fox>().ui_rectScrollNameReference = objectToCreate;
    }

    public void RemoveNameInRectScroll (GameObject rectScrollObject)
    {
        foxNameList.Remove(rectScrollObject);
        GameObject.Destroy(rectScrollObject);
    }

    public void CreateDraggableObject(GameObject objectToCreate)
    {
        Vector3 objectTransform = EventSystem.current.currentSelectedGameObject.transform.position;
        Instantiate(objectToCreate, objectTransform, Quaternion.identity);
        objectToCreate.GetComponent<Drag>().uiLocation = objectTransform;
    }

    public void CreateFloatingUpdate(string textToAssign)
    {
        GameObject objectToCreate = Instantiate(pfab_uiFloatingPopup) as GameObject;
        objectToCreate.transform.SetParent(canvas.transform, false);
        objectToCreate.GetComponentInChildren<Text>().text = textToAssign;
    }

    public void CreateFeedbackIcon(Transform whichTransform, FeedbackIconType whichIconType)
    {
        GameObject objectToCreate = Instantiate(uiFeedbackIcon) as GameObject;
        RectTransform uiReference = objectToCreate.GetComponent<RectTransform>();
        objectToCreate.GetComponent<UI_floatingIcon>().whichIcon = whichIconType;

        //todo Can I get the feedback icon to spawn based on the scal
        objectToCreate.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1);
        
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
        //note = uses a panel as parent with text as child
        objectToCreate.GetComponentInChildren<Text>().text = textToAssign;
        objectToCreate.GetComponent<UIAnchor>().objectToFollow = transformToFollow;

        return objectToCreate;
    }

    //use the fox data to 
    public void CreateFoxInfoPopup(FoxCollectionLog _foxData, float statSpeed, float statNap, float statHunger)
    {
        GameObject objectToCreate = Instantiate(pfab_uiFoxInfoPopup) as GameObject;
        objectToCreate.transform.SetParent(canvas.transform, false);
        //objectToCreate.GetComponent<ui_foxInfoCard>().foxData = _foxData;
        objectToCreate.GetComponent<ui_foxInfoCard>().InitInfoCard(_foxData, statSpeed, statNap, statHunger);

        isPopupActive = true;
    }

    //close the most active popup window 
    public void ClosePopup(GameObject whichObject)
    {
        isPopupActive = false;
        Destroy(whichObject);
    }

    public void OpenScreen(GameObject whichScreen)
    {
        isPopupActive = true;
        whichScreen.SetActive(true);
        //todo make everything but the source button inactive to avoid random button presses
        //todo pause the game
    }

    public void CloseScreen(GameObject whichScreen)
    {
        isPopupActive = false;
        whichScreen.SetActive(false);
        //todo make everything but the source button inactive to avoid random button presses
        //todo unpause the game
    }



    public void ChangeHoverOverState(GameObject whichObject, bool whichState)
    {
        whichObject.SetActive(whichState);
    }

}
