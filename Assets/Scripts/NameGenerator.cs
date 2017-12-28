using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{

    public TextAsset nameTextFile;


    public string[] names;

    public static NameGenerator instance = null;
    public List<string> foxNameList = new List<string>();

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

        names = nameTextFile.text.Split("\n"[0]);
        foxNameList.AddRange(names);
        
    }


    public string GetName()
    {
        string nameToReturn = "";
        int randomVar = Random.Range(0, foxNameList.Count);
        nameToReturn = foxNameList[randomVar];
        foxNameList.Remove(nameToReturn);
        return nameToReturn;
    }

    public void RemoveName(string nameToRemove)
    {
        foxNameList.Remove(nameToRemove);
    }


}
