using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour {

    //pass list of foxes into the game, score, 
	public static void SaveGame(List<GameObject> foxList, int points)
    {
        BinaryFormatter bf = new BinaryFormatter();
        //saves the player file
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        print("Starting process of saving game");
        foreach (GameObject fox in foxList)
        {
            print(fox.GetComponent<Fox>().foxName);
        }

        GameData data = new GameData(foxList, GameController.instance.points);
        print("Saving file with points: " + data.points);
        bf.Serialize(stream, data);
        stream.Close();

    }

    public static bool DoesSaveDataExist()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav")) { return true; }
        else { return false; }

    }

    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            //doesn't actually know what game object it is so you need to cast it as the object
            GameData data = bf.Deserialize(stream) as GameData;

            GameController.instance.SetPoints(data.points);

            //transfers fox data to a new list so the source isn't modified and then creates foxes based on that temp list 
            List<FoxData> foxesToAddListSource = new List<FoxData>();
            foxesToAddListSource = data.foxDataList;
            LoadFoxes(foxesToAddListSource);
           
            stream.Close();
            print("Loading game finished");
        }
        else
        {
            Debug.LogError("File does not exist");
        }
    }

    public static void ClearData()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            File.Delete(Application.persistentDataPath + "/player.sav");
            print("CAUTION: Save data deleted");
        }
    }


        //FYI, this needs to be made static or else I need to call an instance of save manager to use it
        private static void LoadFoxes(List<FoxData> foxSource)
    {
        print("Loading foxes from save data");
        foreach (FoxData foxData in foxSource.ToArray())
        {
            GameController.instance.AddFoxFromData(foxData);
            foxSource.Remove(foxData);
        }
    }

}



[Serializable]
public class GameData
{
    public List<FoxData> foxDataList = new List<FoxData>();
    public int points;

    public GameData(List<GameObject> foxList, int pointsToAdd)
    {
        points = pointsToAdd;

        foreach (GameObject fox in foxList)
        {
            FoxData foxToAdd = new FoxData(fox);
            foxDataList.Add(foxToAdd);
        }

        //apparently I can't have things in here? I can only use the constructor? 
        /*
        foreach(GameObject fox in foxList)
        {
            //ERROR IN GETTING NAMES
            String name = fox.GetComponent<Fox>().foxName;
            foxNames.Add(name);
        }

        points = pointsToAdd;
        */
    }
}

[Serializable]
public class FoxData
{
    public string foxName;
    public string foxType;
    public float moveSpeed;
    public float napTime;
    public float napFrequency;
    public float fullness;
    public float fullnessDecay;

    //probably can't just take the object, access through type
    //public FoxCollectionLog foxLog;

    public FoxData(GameObject foxObject)
    {
        Fox fox = foxObject.GetComponent<Fox>();
        foxName = fox.foxName;
        foxType = fox.foxType;
        moveSpeed = fox.moveSpeed;
        napTime = fox.averageNapTime;
        napFrequency = fox.averageNapApart;
        fullness = fox.fullness;
        fullnessDecay = fox.fullnessDecay;
    }
}
