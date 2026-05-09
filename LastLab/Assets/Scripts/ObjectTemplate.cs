using UnityEngine;

[CreateAssetMenu(fileName = "ObjectTemplate", menuName = "Scriptable Objects/ObjectTemplate")]
public class ObjectTemplate : ScriptableObject
{
    public string objectType;     // тип/имя объекта, например "Кузнец"
    public Sprite objectIcon;    // иконка объекта
    public GameObject objectPrefab; // prefab для создания в сцене
}