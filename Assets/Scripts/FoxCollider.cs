using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo refactor this entire thing into FoxAI or somewhere more relevant
public class FoxCollider : MonoBehaviour {

    public Fox parentFox;
    public FoxAI foxAIController;

    // Use this for initialization
    void Start () {
        parentFox = GetComponentInParent<Fox>();
        foxAIController = GetComponentInParent<FoxAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "food")
        {
            print("Fox ate food");
            parentFox.EatFood(other.gameObject.GetComponent<Food>().pointsValue);
            Destroy(other.gameObject);
            //exit from chase
            //todo where does it actually exit from the chase?
        }
    }



}
