using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_foxInfoRect : MonoBehaviour {

    public GameObject foxObject;
    private Fox foxReference;

    public Text textToDisplay;
    public Slider hungerSlider;
    public Image emotionSprite;
    //public Image sliderFill;
    //public Color maxSliderColor;
    //public Color minSliderColor;

    public Sprite iconHappy;
    public Sprite iconSad;

	// Use this for initialization
	void Start () {
        foxReference = foxObject.GetComponent<Fox>();
        textToDisplay.text = foxReference.foxName;

    }
	
	// Update is called once per frame
	void Update () {

        //update the slider
        hungerSlider.value = foxReference.fullness;

        //REFACTOR: probably turn the 40 into a range supplied by the game manager 
        //display happy emotion
        //sliderFill.color = Color.Lerp(minSliderColor, maxSliderColor, hungerSlider.value / 100);

        if (foxReference.fullness > 40)
        {
            emotionSprite.sprite = iconHappy;
        }
        else
        {
            emotionSprite.sprite = iconSad;
        }
    }
}
