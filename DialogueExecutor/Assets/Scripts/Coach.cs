using Assets.Scripts;
using UnityEngine;

public class Coach : MonoBehaviour, IInteractable
{
    public TextAsset dialogue;
    public DialogueSystem dialogueSystem;
    public PlayerStats playerStats;

    public void interact()
    {
        dialogueSystem.loadDialogue(dialogue);
        dialogueSystem.setAction("increaseIntelligenceByOne", () => IncreaseIntelleginceBy(1));
        dialogueSystem.setAction("increaseIntelligenceByTwo", () => IncreaseIntelleginceBy(2));
        dialogueSystem.setAction("increaseStrengthByOne", () => IncreaseStrengthBy(1));
        dialogueSystem.setAction("increaseStrengthByTwo", () => IncreaseStrengthBy(2));
    }

    private void IncreaseIntelleginceBy(int value)
    {
        playerStats.Intelligence += value;
    }

    private void IncreaseStrengthBy(int value)
    {
        playerStats.Strength += value;
    }
}