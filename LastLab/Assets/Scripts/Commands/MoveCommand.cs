using UnityEngine;

public class MoveCommand : ICommand
{
    GameObject selectedObject;
    Vector3 oldPosition;
    Vector3 newPosition;

    public MoveCommand(GameObject obj, Vector3 oldPos, Vector3 newPos)
    {
        this.selectedObject = obj;
        this.oldPosition = oldPos;
        this.newPosition = newPos;
    }

    public void Execute()
    {
        selectedObject.transform.position = newPosition;
    }

    public void Undo()
    {
        selectedObject.transform.position = oldPosition;
    }
}