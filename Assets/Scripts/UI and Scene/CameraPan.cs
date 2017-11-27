using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraPan : MonoBehaviour {

    private static readonly float PanSpeed = 20f;
    private static readonly float[] BoundsX = new float[] { -10f, 5f };
    private static readonly float[] BoundsZ = new float[] { -18f, -4f };

    int UIlayer = 5;

    private Camera cam;
    private Vector3 lastPanPosition;                                        //last position of user's finger/mouse in last frame when panning
    private int panFingerId; // Touch mode only                             //ID of which finger used to track 
    private bool isInTouch;

    void Awake()
    {
        cam = GetComponent<Camera>();
        UIlayer = 1 << UIlayer;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //todo add a function to check if a menu is open to disable panning
        /*
        if (isMenuOpen)
        {
            return;
        }
        */

        //check if we should handle touch or mouse controls

        if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer) { HandleTouch(); }
        else { HandleMouse(); }
	}

    void HandleTouch()
    {

    }

    void HandleMouse()
    {
        //on mouse down, capture its position, else if mouse is still down, pan camera
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            lastPanPosition = Input.mousePosition;
            isInTouch = true;
        }
        else if (Input.GetMouseButton(0) && !IsPointerOverUIObject())
        {
            if (isInTouch == true)
            {
                PanCamera(Input.mousePosition);

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //release touch
            isInTouch = false;
        }
    }

    private bool OnUILayer()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOnLayer = !Physics.Raycast(ray, out hit, UIlayer);
        print("Is on layer: " + isOnLayer);
        return isOnLayer;
    }

    void PanCamera(Vector3 newPanPosition)
    {
        //determine how much to moe the camera
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed, 0f, offset.y * PanSpeed);

        //execute movement based on world space
        transform.Translate(move, Space.World);

        //make sure it remains within bounds; get a reference to the position and then check if it's within bounds, if it's not, then set it to that position
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);
        transform.position = pos;

        //store the position
        lastPanPosition = newPanPosition;

    }

 


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


}
