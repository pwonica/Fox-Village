using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    public int points = 0;
    public int foxRemovePoints;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RemoveFox()
    {
        //placeholder to do other things
        points -= foxRemovePoints;
        print("You lost a fox!");

    }
}

