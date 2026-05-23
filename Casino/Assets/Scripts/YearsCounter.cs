using System;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent (typeof(TextMeshProUGUI))]
public class YearsCounter : MonoBehaviour
{
    public event Action YearPass;
    public event Action EndRun;
    public int Years => years;

    private int years = 20;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.text = $"Лет до пенсии: {years}/20";
    }

    public void DecreaseYear()
    {
        years -= 1;
        text.text = $"Лет до пенсии: {years}/20";
        YearPass?.Invoke();
        if (years == 0) 
            EndRun?.Invoke();
    }
}
