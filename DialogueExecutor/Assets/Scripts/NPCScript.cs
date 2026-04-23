using Assets.Scripts;
using System;
using UnityEngine;

public class NPCScript : MonoBehaviour, IInteractable
{
    public TextAsset dialogue;           
    public GameObject door;
    public DialogueSystem dialogueSystem;
    public PlayerStats playerStats;

    public void interact()
    {
        dialogueSystem.loadDialogue(dialogue);
        dialogueSystem.setAction("CheckStrength", CheckStrength);
        dialogueSystem.setAction("CheckIntelligence", CheckIntelligence);
    }

    private void CheckIntelligence()
    {
        if (playerStats.Intelligence >= 2)
        {
            openDoor();
        }
    }

    private void CheckStrength()
    {
        if (playerStats.Strength >= 2)
        {
            openDoor();
        }
    }

    public void openDoor()
    {
        door.GetComponent<Animator>().SetBool("isOpen", true);
    }
}
