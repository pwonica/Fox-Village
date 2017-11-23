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

        GameData data = new GameData(foxList, 100);
        bf.Serialize(stream, data);
        stream.Close();

    }

    public static List<String> LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            //doesn't actually know what game object it is so you need to cast it as the object
            GameData data = bf.Deserialize(stream) as GameData;
            stream.Close();
            return data.foxNames;
        }
        else
        {
            Debug.LogError("File does not exist");
            return new List<string>(); 
        }
    }


}

[Serializable]
public class GameData
{
    public List<String> foxNames;
    public int points;

    public GameData(List<GameObject> foxList, int pointsToAdd)
    {
        foreach(GameObject fox in foxList)
        {
            foxNames.Add(fox.GetComponent<Fox>().foxName);
        }

        points = pointsToAdd;
    }
}
