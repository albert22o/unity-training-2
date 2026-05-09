using UnityEngine;
using UnityEngine.UI;

public class LoadCommand : ICommand
{
    UIBuilder builder;
    ObjectTemplate template;
    SaveData saveData;
    GameObject newObject;

    public LoadCommand(UIBuilder builder, ObjectTemplate template, SaveData saveData)
    {
        this.builder = builder;
        this.template = template;
        this.saveData = saveData;
    }

    public void Execute()
    {
        newObject = Object.Instantiate(template.objectPrefab);
        ObjectDescription objectDescr = newObject.AddComponent<ObjectDescription>();

        saveData.getDescription(objectDescr);   // применяем сохранённые данные

        GameObject button = Object.Instantiate(
            builder.objectReferenceButtonPrefab, builder.referenceButtonsContainer);
        newObject.name = objectDescr.objectName;
        button.GetComponent<ObjectButtonScript>().setText(objectDescr.objectName);
        button.GetComponent<Button>().onClick.AddListener(
            delegate { builder.cursor.select(newObject); });

        objectDescr.objectName = newObject.name;
        objectDescr.template = template;
        objectDescr.referenceButton = button;

        builder.addObject(objectDescr);
    }

    public void Undo() { /* загрузка не поддерживает отмену */ }
}