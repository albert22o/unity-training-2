using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Deposit : MonoBehaviour
{
    public int DepositValue => depositValue;

    [SerializeField] MoneyCounter moneyCounter;
    [SerializeField] YearsCounter yearsCounter;
    [SerializeField] Animator animator;
    private int depositIncome => depositValue * 4 / 100;
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
        if (moneyCounter.Money < 1000)
            return;
        animator.Play("Piggy");
        moneyCounter.Money -= 1000;
        depositValue += 1000;
        UpdateText();
    }

    public void RemoveHundred()
    {

        if (depositValue < 1000)
            return;
        animator.Play("PiggyRevers");
        moneyCounter.Money += 1000;
        depositValue -= 1000;
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
