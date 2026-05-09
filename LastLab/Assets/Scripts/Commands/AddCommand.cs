using UnityEngine;
using UnityEngine.UI;

public class AddCommand : ICommand
{
    UIBuilder builder;
    ObjectTemplate template;
    GameObject newObject;

    public AddCommand(UIBuilder builder, ObjectTemplate template)
    {
        this.builder = builder;
        this.template = template;
    }

    public void Execute()
    {
        newObject = Object.Instantiate(template.objectPrefab);
        newObject.transform.position = new Vector3(
            Random.Range(-15, 15), 0, Random.Range(-15, 15));

        ObjectDescription objectDescr = newObject.AddComponent<ObjectDescription>();

        GameObject button = Object.Instantiate(
            builder.objectReferenceButtonPrefab, builder.referenceButtonsContainer);
        newObject.name = button.name;
        button.GetComponent<ObjectButtonScript>().setText(newObject.name);
        button.GetComponent<Button>().onClick.AddListener(
            delegate { builder.cursor.select(newObject); });

        objectDescr.objectName = newObject.name;
        objectDescr.template = template;
        objectDescr.referenceButton = button;

        builder.addObject(objectDescr);
    }

    public void Undo()
    {
        builder.cursor.deselect();
        Transform button = newObject.GetComponent<ObjectDescription>().referenceButton.transform;

        for (int i = 0; i < builder.referenceButtonsContainer.childCount; i++)
        {
            Transform child = builder.referenceButtonsContainer.GetChild(i);
            if (child == button)
            {
                Object.Destroy(child.gameObject);
                break;
            }
        }
        Object.Destroy(newObject);
    }
}