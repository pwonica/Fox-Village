using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public SaveManager saveManager;
    public Text txt_IsSaveActive;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        txt_IsSaveActive.text = SaveManager.DoesSaveDataExist().ToString();

    }
}
