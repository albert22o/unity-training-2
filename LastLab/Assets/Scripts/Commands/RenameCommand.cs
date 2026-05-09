public class RenameCommand : ICommand
{
    ObjectDescription selectedObject;
    string oldName;
    string newName;

    public RenameCommand(ObjectDescription obj, string oldName, string newName)
    {
        this.selectedObject = obj;
        this.oldName = oldName;
        this.newName = newName;
    }

    public void Execute()
    {
        if (selectedObject != null) selectedObject.updateName(newName);
    }

    public void Undo()
    {
        if (selectedObject != null) selectedObject.updateName(oldName);
    }
}