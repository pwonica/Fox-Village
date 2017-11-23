using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;

	void Start(){

		//used for splash screens
		if (autoLoadNextLevelAfter == 0){
			Debug.Log ("Level auto load disabled");
		}
		else{
			//used to load the next level after the splash screen
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}

	}

    //todo update this to Scene Manager usage
	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		//Application.LoadLevel (name);
        
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		//Application.Quit ();
	}

	public void LoadNextLevel(){
		//Application.LoadLevel (Application.loadedLevel + 1);

	}



}
