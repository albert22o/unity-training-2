using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LuckLevel : MonoBehaviour
{
    public event Action Change;
    public int Luck
    {
        get => luck;
        set
        {
            luck = value;
            text.text = $"Удача: {luck} ур.";
            Change?.Invoke();
        }
    }

    private int luck = 1;
    private TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Luck = luck;
    }
}
