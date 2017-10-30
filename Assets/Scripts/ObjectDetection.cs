using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectDetection : MonoBehaviour {

    public string tagToDetect;
    public bool objectDetected = false;
    public Transform targetObject;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToDetect)
        {
            objectDetected = true;
            targetObject = other.transform;
        }
       

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == tagToDetect)
        {
            objectDetected = false;
            targetObject = null;
            //this.GetComponentInParent<Fox>().OnCollisionExitChild();
        }

    }
    
}
