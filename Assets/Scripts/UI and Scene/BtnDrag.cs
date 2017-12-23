using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BtnDrag : MonoBehaviour {

    public bool isDragging = false;
    public GameObject objectDragging;
    public int itemCost;

	// Use this for initialization
	void Start () {
		
	}

    public void HandleDrag(GameObject objectToCreate)
    {
        if (!isDragging)
        {
            print("Creating draggabe food");
            //create at your mouse location
            Vector3 objectTransform = EventSystem.current.currentSelectedGameObject.transform.position;
            objectDragging = Instantiate(objectToCreate, objectTransform, Quaternion.identity);
            objectToCreate.GetComponent<Drag>().uiButton = this.GetComponent<BtnDrag>();
            objectToCreate.GetComponent<Drag>().uiLocation = objectTransform;
        }
    }

    public void ResetDrag()
    {
        isDragging = false;
    }
    
}
