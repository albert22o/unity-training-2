using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public string objectName;
    public string objectType;
    public Vector3 position;
    public Quaternion rotation;

    public SaveData(ObjectDescription od)
    {
        objectName = od.objectName;
        objectType = od.template.objectType;
        position = od.transform.position;
        rotation = od.transform.rotation;
    }

    // Заполняет существующий ObjectDescription данными из файла
    public void getDescription(ObjectDescription od)
    {
        od.objectName = objectName;
        od.transform.position = position;
        od.transform.rotation = rotation;
    }
}

// Обёртка для сериализации списка через JsonUtility
[Serializable]
public class DataList
{
    public System.Collections.Generic.List<SaveData> list;
}