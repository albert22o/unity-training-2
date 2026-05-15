using System.Collections.Generic;
using UnityEngine;

public class DialogueButtonsTracker : MonoBehaviour
{
    [SerializeField] private DialogueButton[] buttons;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject hide;

    private HashSet<DialogueButton> clicked = new HashSet<DialogueButton>();

    void Start()
    {
        foreach (var button in buttons)
        {
            button.OnClick += OnButtonClicked;
        }
    }

    void OnButtonClicked(DialogueButton button)
    {
        clicked.Add(button);

        if (clicked.Count == buttons.Length)
        {
            target.SetActive(true);
            if (hide  != null) 
                hide.SetActive(false);
        }
    }

    void OnDestroy()
    {
        foreach (var button in buttons)
        {
            button.OnClick -= OnButtonClicked;
        }
    }
}