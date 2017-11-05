using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxUIDisplay : MonoBehaviour {

    public GameObject foxObject;
    private Fox foxReference;

    private Text textToDisplay;

	// Use this for initialization
	void Start () {
        textToDisplay = GetComponent<Text>();
        foxReference = foxObject.GetComponent<Fox>();
	}
	
	// Update is called once per frame
	void Update () {
        textToDisplay.text = foxReference.foxName + " ||| Fullness: " + foxReference.fullness;
	}
}
