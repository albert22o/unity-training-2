using UnityEngine;
using TMPro;

public class DialogueText : MonoBehaviour
{
    [SerializeField] private GameObject dialogueWindow;
    [SerializeField] private TextMeshProUGUI nameComponent;
    [SerializeField] private TextMeshProUGUI textComponent;

    private int currentLine = 0;
    private bool isActive = false;
    private DialogueLine[] lines;

    void Update()
    {
        if (isActive && Input.GetMouseButtonDown(0))
        {
            ShowNextLine();
        }
    }

    public void StartDialogue(DialogueLine[] newLines)
    {
        if (isActive) return;

        lines = newLines;
        OpenDialogue();
    }

    void OpenDialogue()
    {
        currentLine = 0;
        isActive = true;
        dialogueWindow.SetActive(true);
        ShowNextLine();
    }

    void ShowNextLine()
    {
        if (currentLine >= lines.Length)
        {
            isActive = false;
            dialogueWindow.SetActive(false);
            return;
        }

        nameComponent.text = lines[currentLine].characterName;
        textComponent.text = lines[currentLine].text;
        currentLine++;
    }
}

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    public string text;
}