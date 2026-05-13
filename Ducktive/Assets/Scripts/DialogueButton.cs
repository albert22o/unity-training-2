using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DialogueButton : MonoBehaviour, IPointerClickHandler
{
    public event Action<DialogueButton> OnClick;
    [SerializeField] private UnityEvent Click;
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueLine[] lines;

    public void OnPointerClick(PointerEventData eventData)
    {
        dialogueText.StartDialogue(lines);
        OnClick?.Invoke(this);
        Click?.Invoke();
    }
}