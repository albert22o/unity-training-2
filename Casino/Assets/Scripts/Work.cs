using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Work : MonoBehaviour
{
    [SerializeField] MoneyCounter moneyCounter;
    [SerializeField] YearsCounter yearsCounter;
    [SerializeField] Salary salary;

    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        if (moneyCounter == null || yearsCounter == null || salary == null)
            Debug.LogError("Проставь все зависимости");
    }

    private void Start()
    {
        button.onClick.AddListener(() => { OnClick(); });
    }

    private void OnClick()
    {
        if (yearsCounter.Years <= 0)
            return;
        yearsCounter.DecreaseYear();
        moneyCounter.Money += salary.Amount;
    }
}

