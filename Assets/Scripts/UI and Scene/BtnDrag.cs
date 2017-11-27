using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BtnDrag : MonoBehaviour {

    public bool isDragging = false;
    public GameObject objectDragging;

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
            objectToCreate.GetComponent<Drag>().uiLocation = objectTransform;
            objectToCreate.GetComponent<Drag>().uiButton = this;
            isDragging = true;
            //attach it to your hold
        }
    }
    
}
