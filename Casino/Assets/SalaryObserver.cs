using System;
using TMPro;
using UnityEngine;

public class SalaryObserver : MonoBehaviour
{
    [SerializeField] Salary salary;
    [SerializeField] TextMeshProUGUI text;
    private void Awake()
    {
        salary.AmountChanged += OnAmountChanged;
    }

    private void OnAmountChanged(int amount)
    {
        text.text = $"Зарплата: +{amount}";
    }
}
