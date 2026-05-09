using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadSystem
{
    public static void SaveToFile(string fileName, List<SaveData> saveData)
    {
        DataList saveList = new DataList();
        saveList.list = saveData;
        string json = JsonUtility.ToJson(saveList);
        File.WriteAllText(fileName, json);
    }

    public static List<SaveData> LoadFromFile(string fileName)
    {
        string json = File.ReadAllText(fileName);
        DataList loadList = JsonUtility.FromJson<DataList>(json);
        return loadList.list;
    }
}