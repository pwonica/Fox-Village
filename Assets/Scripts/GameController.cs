using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    public int points = 0;
    public int pointAddTimer;
    public int pointsPerFox;
    public int pointsBaseRate;
    public int foxRemovePoints;
    public int hungryDecreaseRate;                              //how many points are removed per hungry fox
    public int foxPurCHASECost;

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
        InvokeRepeating("AddRegularPoints", 0f, pointAddTimer);

    }
    // Update is called once per frame
    void Update () {
		
	}

    private void AddRegularPoints()
    {
        AddPoints(pointsBaseRate + (pointsPerFox * foxList.Count));
    }

    public void AddFox()
    {
        GameObject foxToCreate = Instantiate(pfabFox, foxSpawnLocation.position, Quaternion.identity);
        foxList.Add(foxToCreate);
        string textToCreate = "You adopted " + foxToCreate.GetComponent<Fox>().foxName + "!";
        UIManager.instance.CreatePopupText(textToCreate);

    }

    public void PurCHASEFox()
    {
        if (points > foxPurCHASECost)
        {
            AddFox();
            SubtractPoints(foxPurCHASECost);
        }
        else
        {
            //play error sound
        }

    }

    public void RemoveFox(GameObject foxToRemove)
    {
        //placeholder to do other things
        string textToCreate = foxToRemove.GetComponent<Fox>().foxName + " ran away!";
        UIManager.instance.CreatePopupText(textToCreate);
        foxList.Remove(foxToRemove);
        GameObject.Destroy(foxToRemove);
        SubtractPoints(foxRemovePoints);

        print("You lost a fox!");
        

    }

    public void AddPoints(int valueToAdd) {
        points += valueToAdd;
        UIManager.instance.UpdatePoints();
    }
    public void SubtractPoints(int valueToReduce) {
        points -= valueToReduce;
        UIManager.instance.UpdatePoints();
        if(points < 0)
        {
            points = 0;
        }
    }


}

