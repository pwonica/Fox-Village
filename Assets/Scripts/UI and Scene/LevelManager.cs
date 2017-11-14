using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;
    public Sprite[] tutorialImages;
    private int onWhichTutorialImage;


    void Start(){
        /*
		//used for splash screens
		if (autoLoadNextLevelAfter == 0){
			Debug.Log ("Level auto load disabled");
		}
		else{
			//used to load the next level after the splash screen
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
        */
	}


	public void LoadLevel(string name){
        SceneManager.LoadScene(name);
    }

    public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadNextLevel(){

	}

    public void NextTutorialImage()
    {
        
    }


}
