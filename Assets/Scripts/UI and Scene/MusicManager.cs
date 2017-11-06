using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;
	
	private AudioSource audioSource;

	//makes sure this object persists between scenes
	void Awake(){
		DontDestroyOnLoad(gameObject);
		Debug.Log ("Don't destroy on load: " + name);
	}

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	
	}

	void OnLevelWasLoaded(int level){

		AudioClip thisLevelMusic = levelMusicChangeArray[level];
		Debug.Log ("Playing clip: " + thisLevelMusic);

		//if there is actually some music attached to this varable (ex: if there's nothing in the array on that level)
		if (thisLevelMusic){
			//assigns the audio clip from the array to our music player's audio source so it can play
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play ();
		}

	}

}
