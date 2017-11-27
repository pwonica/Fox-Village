using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    public float returnDistanceOffset;
    public GameObject uiObject;                                 //object used to just display it 
    public BtnDrag uiButton;                                 //reference to the UI button that it's connected to
    public GameObject objectToCreatePfab;                       //object that's created 
    private float yOriginal;
    public float yOffset;

    private bool isMouseDrag = false;
    private bool canUse = true;
    private float cooldownTimer;
    public float cooldownRate;                                 //how often you can use it

    private bool originalLocationSet;
    private Vector3 originalLocation;
    public Vector3 uiLocation;




    private void Start()
    {
        yOriginal = transform.position.y;
        uiLocation = transform.position;
    }



    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CreateObject();
        }
        else
        {
            MoveBasedOnMouse();
        }

    }

    private void MoveBasedOnMouse()
    {
        //move based on 
        Plane plane = new Plane(Vector3.up, new Vector3(0, yOffset, 0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            transform.position = ray.GetPoint(distance);
        }

        if (!originalLocationSet)
        {
            originalLocation = transform.position;
            originalLocation.y = yOffset;
            originalLocationSet = true;
        }
    }

    /*
    private void OnMouseDrag()
    {
        //move based on 
        Plane plane = new Plane(Vector3.up, new Vector3(0, yOffset, 0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            transform.position = ray.GetPoint(distance);
        }

        if (!originalLocationSet)
        {
            originalLocation = transform.position;
            originalLocation.y = yOffset;
            originalLocationSet = true;
        }
        
          
    }
    */

    //check if it's at it's original point and then return to the element, if not, then drop in place
    private void CreateObject()
    {
        //if it's not within the range of the origin point, then reduce 
        if (!((transform.position - originalLocation).magnitude < returnDistanceOffset))
        {
            print("Dropping food in world");
            Instantiate(objectToCreatePfab, transform.position, Quaternion.identity);
            transform.position = uiLocation;
            uiButton.isDragging = false;
            Destroy(gameObject);
        }
        ResetDragButton();
    }

    private void ResetDragButton()
    {
        uiButton.isDragging = false;
        Destroy(gameObject);
    }



}
