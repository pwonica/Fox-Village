using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectDetection : MonoBehaviour {

    public string tagToDetect;
    public bool objectDetected = false;
    public Transform targetObject;

    public FoxAI foxAIController;


	// Use this for initialization
	void Start () {
        foxAIController = GetComponentInParent<FoxAI>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        //if notices the fox, don't do anything

        
            if (other.tag == tagToDetect)
            {
                objectDetected = true;
                targetObject = other.transform;
                foxAIController.EnterCHASE();
            }
      
        /*
        if (other.tag == tagToDetect)
        {
            objectDetected = true;
            targetObject = other.transform;
            foxAIController.EnterCHASE();
        }
       */

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == tagToDetect)
        {
            objectDetected = false;
            targetObject = null;
            foxAIController.ExitCHASE();
            //this.GetComponentInParent<Fox>().OnCollisionExitChild();
        }

    }
    
}
