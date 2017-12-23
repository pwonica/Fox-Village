using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    public int points = 0;
    public int foxRemovePoints;
    public int hungryDecreaseRate;                              //how many points are removed per hungry fox
    public int foxPurchaseCost;
    public int foodPurchaseCost;
    public int passivePointBoost_fox;
    public int passivePointBoost_base;
    public int passivePointBoost_time;

    public float passiveBoostTimer;

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
        //AddFox();
        passiveBoostTimer = passivePointBoost_time;

        //check if there's active save data, if so load from the save data

    }
    // Update is called once per frame
    void Update () {

        if (passiveBoostTimer <= 0)
        {
            PassivePointBoost();
            passiveBoostTimer = passivePointBoost_time;
        }
        passiveBoostTimer -= Time.deltaTime;
    }

    public void Save()
    {
        SaveManager.SaveGame(foxList, points);
    }

    public void Load()
    {
        SaveManager.LoadGame();
    }

    //at some point, condense the add fox into one single thing
    public void AddFox()
    {
        GameObject foxToCreate = Instantiate(pfabFox, foxSpawnLocation.position, Quaternion.identity);
        foxList.Add(foxToCreate);
    }

    public void AddFoxFromData(FoxData foxData)
    {
        GameObject foxToCreate = Instantiate(pfabFox, foxSpawnLocation.position, Quaternion.identity);
        Fox fox = foxToCreate.GetComponent<Fox>();
        fox.foxName = foxData.foxName;
        print("Creating fox from sava data: " + fox.foxName);
        foxList.Add(foxToCreate);

    }


    public void PurchaseFox()
    {
        if (points > foxPurchaseCost)
        {
            AddFox();
            SubtractPoints(foxPurchaseCost);
        }
        else
        {
            //play error sound
        }

    }


    public void PurchaseFood(int itemCost)
    {
        SubtractPoints(itemCost);
    }
    public void RemoveFox(GameObject foxToRemove)
    {
        //placeholder to do other things
        foxList.Remove(foxToRemove);
        GameObject.Destroy(foxToRemove);
        SubtractPoints(foxRemovePoints);
        print("You lost a fox!");
        

    }

  

    //adds points based on number of foxes that you have
    //TODO can add stats (ex: you only get points for happy foxes for example)
    private void PassivePointBoost()
    {
        int pointsToAdd = (foxList.Count * passivePointBoost_fox) + passivePointBoost_base;
        print("Adding points: " + pointsToAdd);
        AddPoints(pointsToAdd);
    }

    public void AddPoints(int valueToAdd) {
        points += valueToAdd;
        UIManager.instance.UpdatePoints();
    }
    public void SubtractPoints(int valueToReduce) {
        points -= valueToReduce;
        UIManager.instance.UpdatePoints();
    }
    public void SetPoints(int valueToSet)
    {
        points = valueToSet;
        UIManager.instance.UpdatePoints();
    }


}

