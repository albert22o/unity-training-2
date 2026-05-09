using UnityEngine;

public class RotateCommand : ICommand
{
    ObjectDescription selectedObject;
    Vector3 oldRotation;
    Vector3 newRotation;

    public RotateCommand(ObjectDescription obj, Vector3 oldRot, Vector3 newRot)
    {
        this.selectedObject = obj;
        this.oldRotation = oldRot;
        this.newRotation = newRot;
    }

    public void Execute()
    {
        if (selectedObject != null)
            selectedObject.transform.eulerAngles = newRotation;
    }

    public void Undo()
    {
        if (selectedObject != null)
            selectedObject.transform.eulerAngles = oldRotation;
    }
}