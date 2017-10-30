using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxCollider : MonoBehaviour {

    public Fox parentFox;

	// Use this for initialization
	void Start () {
        parentFox = GetComponentInParent<Fox>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "food")
        {
            print("Fox ate food");
            parentFox.EatFood(other.gameObject.GetComponent<Food>().pointsValue);           
            Destroy(other.gameObject);
        }
    }
}
