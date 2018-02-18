using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fox Collection maintains the collection of foxes (such as the 3d models and a data class for communicating about what type (ex: name, rarity, cost) 
public class FoxCollection : MonoBehaviour {

    public static FoxCollection instance = null;

    //todo figure out how to get a system that includes just one means of storing all of this
    public List<Transform> foxMeshList = new List<Transform>();
    public List<Sprite> foxImageList = new List<Sprite>();
    public List<FoxCollectionLog> foxDatabase = new List<FoxCollectionLog>();



    // Use this for initialization
    void Awake () {
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

    //imports from the attached CSV file
    private void ImportFoxLog()
    {

    }

    public Transform GetRandomFox()
    {
        int whichOne = UnityEngine.Random.Range(0, foxDatabase.Count);
        Transform foxModel = foxMeshList[whichOne];
        return foxModel;
    }

    //gets a fox from the database based on it's type
    public FoxCollectionLog GetFoxFromCollection(string whichFox)
    {
        FoxCollectionLog foxLog = new FoxCollectionLog();
        print("searching for fox of type " + whichFox);
        //search through database for specific fox 
        for (int i = 0; i < foxDatabase.Count; i++)
        {
            if (foxDatabase[i].foxType == whichFox)
            {
                print("Found fox");
                foxLog = foxDatabase[i];
            }
            /*
            else
            {
                Debug.LogError("Error! Cannot find fox type");
                return null;
            }
            */
        }

        //todo this needs a means of checking for a null fox 
        return foxLog;


    }


}


[Serializable]
//log stores the name of the fox, model name, rarity, and cost. This is imported by CSV data. It is used whenever wanted to create/maintain foxes
public class FoxCollectionLog
{
    public string foxType;
    public Transform foxMesh;
    //todo add a image, will find by name 
    public Sprite foxSprite;
    public float rarity;
    public float cost;

    //todo Remove this, technically it doesn't need a constructor if not doing by data
    /*
    public FoxCollectionLog(string _foxType, string meshName, string imageName, float _rarity, float _cost)
    {
        foxType = _foxType;
        rarity = _rarity;
        cost = _cost;
    }
    */
}
