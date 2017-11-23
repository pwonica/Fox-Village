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
	
    void OnTriggerEnter(Collider other)
    {
        //if notices the fox, don't do anything
            if (other.tag == tagToDetect)
            {
                objectDetected = true;
                targetObject = other.transform;
                foxAIController.EnterChase();
            }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == tagToDetect)
        {
            objectDetected = false;
            targetObject = null;
            foxAIController.ExitChase();
            //this.GetComponentInParent<Fox>().OnCollisionExitChild();
        }

    }

}
