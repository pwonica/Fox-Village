using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    public float returnDistanceOffset;
    public GameObject uiObject;                                 //object used to just display it     
    public GameObject objectToCreatePfab;                       //object that's created 
    private float yOriginal;
    public float yOffset;

    private bool isMouseDrag = false;
    private bool canUse = true;
    private float cooldownTimer;
    public float cooldownRate;                                 //how often you can use it

    private bool originalLocationSet;
    private Vector3 originalLocation;
    private Vector3 uiLocation;




    private void Start()
    {
        yOriginal = transform.position.y;
        uiLocation = transform.position;
    }

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


    //check if it's at it's original point and then return to the element, if not, then drop in place
    private void OnMouseUp()
    {
        //if it's not within the range of the origin point, then reduce 
        if (!((transform.position - originalLocation).magnitude < returnDistanceOffset))
        {
            print("Creating new objecT");
            Instantiate(objectToCreatePfab, transform.position, Quaternion.identity);
            transform.position = uiLocation;
        }


        ResetPosition();

        //if it's near the original collision area, display an X icon above it to indicate you're deleting the item

        //if it's not near the collision area, it's in the world space, create an object within the area and set it in 
    }

    private void ResetPosition()
    {
        print("Returning to original location");
        transform.position = uiLocation;
        originalLocationSet = false;
    }

    /*
    void OnMouseDown()
    {
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(mouse);
    }

    /**
     * Mouse is dragged
     */

    /*
void OnMouseDrag()
{
    Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    curPosition.y = 2f;
    transform.position = curPosition;
}

/*
private void OnMouseDrag()
{
    Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
    Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

    transform.position = objPosition;
}
*/

}
