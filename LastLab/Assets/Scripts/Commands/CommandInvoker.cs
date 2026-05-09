using System.Collections.Generic;

public class CommandInvoker
{
    const int STACK_LIMIT = 10;  // максимум команд в стеке

    static LinkedList<ICommand> undoStack = new LinkedList<ICommand>();
    static Stack<ICommand> redoStack = new Stack<ICommand>();

    public static void ExeqteCommand(ICommand command)
    {
        command.Execute();

        undoStack.AddLast(command);

        // Если превысили лимит — удаляем самую старую команду
        if (undoStack.Count > STACK_LIMIT)
            undoStack.RemoveFirst();

        // Любое новое действие сбрасывает redo-стек
        redoStack.Clear();
    }

    public static void undoCommand()
    {
        if (undoStack.Count == 0) return;

        ICommand lastCommand = undoStack.Last.Value;
        undoStack.RemoveLast();
        lastCommand.Undo();

        redoStack.Push(lastCommand);
    }

    public static void redoCommand()
    {
        if (redoStack.Count == 0) return;

        ICommand command = redoStack.Pop();
        command.Execute();
        undoStack.AddLast(command);

        if (undoStack.Count > STACK_LIMIT)
            undoStack.RemoveFirst();
    }

    public static void ClearStack()
    {
        undoStack.Clear();
        redoStack.Clear();
    }
}