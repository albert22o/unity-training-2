using System.Collections.Generic;
using System.Windows.Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIBuilder : MonoBehaviour
{
    [SerializeField] GameObject objectButtonPrefab;    // шаблон кнопки добавления
    [SerializeField] Transform buttonsContainer;        // контейнер правой панели
    public ObjectTemplate[] objectTemplates;              // заполняется автоматически

    // Добавляется в Лаб.2:
    [SerializeField] public GameObject objectReferenceButtonPrefab;
    [SerializeField] public Transform referenceButtonsContainer;
    [SerializeField] public Cursor cursor; // добавляется в Лаб.2

    // Список объектов сцены (добавляется в Лаб.4)
    public List<ObjectDescription> objectsList = new List<ObjectDescription>();

    void Start()
    {
        // Загрузка всех шаблонов из папки Resources/Objects
        objectTemplates = Resources.LoadAll<ObjectTemplate>("Objects");

        foreach (ObjectTemplate template in objectTemplates)
        {
            GameObject button = Instantiate(objectButtonPrefab, buttonsContainer);
            button.GetComponent<ObjectButtonScript>().setText(template.objectType);
            button.GetComponent<ObjectButtonScript>().setSprite(template.objectIcon);
            button.GetComponent<Button>().onClick.AddListener(delegate { createObject(template); });
        }
    }

    // Метод изменится в Лаб.3 (будет использовать команды)
    public void createObject(ObjectTemplate template)
    {
        ICommand command = new AddCommand(this, template);
        CommandInvoker.ExeqteCommand(command);
    }

    // Добавляется в Лаб.4:
    public void addObject(ObjectDescription objectDescription)
    {
        objectsList.Add(objectDescription);
    }

    public void Undo()
    {
        CommandInvoker.undoCommand();
    }


    // Получить список данных для сохранения из всех активных объектов
    List<SaveData> getSaveData()
    {
        List<SaveData> saveList = new List<SaveData>();
        foreach (ObjectDescription item in objectsList)
        {
            if (item.gameObject.activeSelf)
                saveList.Add(item.getData());
        }
        return saveList;
    }

    // Поиск шаблона по типу объекта
    ObjectTemplate findTemplateByType(string type)
    {
        foreach (ObjectTemplate template in objectTemplates)
            if (template.objectType == type) return template;
        return null;
    }

    // Очистка всех объектов сцены
    public void sceneClear()
    {
        foreach (ObjectDescription descr in objectsList)
        {
            Object.Destroy(descr.referenceButton);
            Object.Destroy(descr.gameObject);
        }
        objectsList.Clear();
    }
    public void Redo()
    {
        CommandInvoker.redoCommand();
    }

    // Сохранение сцены в файл
    public void saveToFile()
    {
        string fileName = EditorUtility.SaveFilePanel(
            "Save scene as json", "", "scene" + ".json", "json");
        SaveLoadSystem.SaveToFile(fileName, getSaveData());
    }

    // Загрузка сцены из файла
    public void loadFromFile()
    {
        string fileName = EditorUtility.OpenFilePanel("Load scene from json", "", "json");

        List<SaveData> saveList = SaveLoadSystem.LoadFromFile(fileName);
        sceneClear();

        foreach (SaveData data in saveList)
        {
            ObjectTemplate template = findTemplateByType(data.objectType);
            if (template == null) continue;
            ICommand command = new LoadCommand(this, template, data);
            CommandInvoker.ExeqteCommand(command);
        }

        CommandInvoker.ClearStack();  // загрузка не поддерживает отмену
    }
}