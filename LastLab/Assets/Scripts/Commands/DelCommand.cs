using UnityEngine;

public class DelCommand : ICommand
{
    GameObject selectedObject;

    public DelCommand(GameObject obj)
    {
        this.selectedObject = obj;
    }

    public void Execute()
    {
        if (selectedObject == null) return;
        selectedObject.SetActive(false);
        selectedObject.GetComponent<ObjectDescription>().referenceButton.SetActive(false);
    }

    public void Undo()
    {
        if (selectedObject == null) return;
        selectedObject.SetActive(true);
        selectedObject.GetComponent<ObjectDescription>().referenceButton.SetActive(true);
    }
}