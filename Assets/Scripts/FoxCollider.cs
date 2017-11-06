using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxCollider : MonoBehaviour {

    public Fox parentFox;
    public string tagToDetect;
    public bool objectDetected = false;
    public Transform targetObject;

    public FoxAI foxAIController;

    // Use this for initialization
    void Start () {
        parentFox = GetComponentInParent<Fox>();
        foxAIController = GetComponentInParent<FoxAI>();


    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "food")
        {
            print("Fox ate food");
            parentFox.EatFood(collision.gameObject.GetComponent<Food>().pointsValue);
            Destroy(collision.gameObject);
            //exit from chase
        }
   
    }
    

}
