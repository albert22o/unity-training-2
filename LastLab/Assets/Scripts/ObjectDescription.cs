using UnityEditor.Overlays;
using UnityEngine;

public class ObjectDescription : MonoBehaviour
{
    public string objectName;
    public ObjectTemplate template;
    public GameObject referenceButton;  

    public void updateName(string newName)
    {
        objectName = newName;
        referenceButton.GetComponent<ObjectButtonScript>().setText(newName);
        gameObject.name = newName;
    }

    public SaveData getData()
    {
        SaveData data = new SaveData(this);
        return data;
    }
}