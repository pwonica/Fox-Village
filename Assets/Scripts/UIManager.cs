using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;

    public Text txtPoints;

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

    // Use this for initialization
    void Start () {
        UpdatePoints();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdatePoints()
    {
        txtPoints.text = GameController.instance.points.ToString();
    }
}
