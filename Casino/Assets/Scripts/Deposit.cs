using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Deposit : MonoBehaviour
{
    public int DepositValue => depositValue;

    [SerializeField] MoneyCounter moneyCounter;
    [SerializeField] YearsCounter yearsCounter;

    private int depositIncome => depositValue * 2 / 10;
    private TextMeshProUGUI text;
    private int depositValue = 0;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (moneyCounter == null || yearsCounter == null)
            Debug.LogError("Проставь все зависимости");
    }

    private void Start()
    {
        UpdateText();
        yearsCounter.YearPass += OnYearPass;
    }

    public void AddHundred()
    {
        if (moneyCounter.Money < 100)
            return;
        moneyCounter.Money -= 100;
        depositValue += 100;
        UpdateText();
    }

    public void RemoveHundred()
    {
        if (depositValue < 100)
            return;
        moneyCounter.Money += 100;
        depositValue -= 100;
        UpdateText();
    }

    private void OnYearPass()
    {
        moneyCounter.Money += depositIncome;
    }

    private void UpdateText()
    {
        text.text = $"На вкладе сейчас: {depositValue}\r\nОн принесет: {depositIncome}";
    }
}
