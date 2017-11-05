﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    public int points = 0;
    public int foxRemovePoints;
    public int foxPurchaseCost;

    public Transform foxSpawnLocation;
    public GameObject pfabFox;

    public List<GameObject> foxList = new List<GameObject>();


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


    private void Start()
    {
        AddFox();
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void AddFox()
    {
        GameObject foxToCreate = Instantiate(pfabFox, foxSpawnLocation.position, Quaternion.identity);
        foxList.Add(foxToCreate);

    }

    public void PurchaseFox()
    {
        if (points > foxPurchaseCost)
        {
            AddFox();
            points -= foxPurchaseCost;
        }
        else
        {
            //play error sound
        }

    }

    public void RemoveFox(GameObject foxToRemove)
    {
        //placeholder to do other things
        foxList.Remove(foxToRemove);
        GameObject.Destroy(foxToRemove);
        points -= foxRemovePoints;
        print("You lost a fox!");
        

    }

    public void AddPoints(int valueToAdd) {
        points += valueToAdd;
        UIManager.instance.UpdatePoints();
    }
    public void SubtractPoints(int valueToReduce) {
        points -= valueToReduce;
        UIManager.instance.UpdatePoints();
    }


}

