using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Fade : MonoBehaviour {

	public float fadeInTime;

	private Image fadePanel;
	private Color currentColor = Color.black; 				//need to form color before passing it
	// Use this for initialization
	void Start () {
		fadePanel = GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad < fadeInTime){
			//fadeIn
			float alphaChange = Time.deltaTime / fadeInTime; //how much we change the frame 
			currentColor.a -= alphaChange; //reduces alpha
			fadePanel.color = currentColor;
		}
		else{
			//deactivates panel so you can click buttons 
			gameObject.SetActive (false);
		}
	}
}
