using System;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalText;
    [SerializeField] GameObject panel;
    [SerializeField] YearsCounter yearsCounter;
    [SerializeField] MoneyCounter moneyCounter;
    [SerializeField] Deposit deposit;

    private void Start()
    {
        yearsCounter.EndRun += ShowGameOver;
    }

    private void ShowGameOver()
    {
        panel.SetActive(true);
        var finalScore = moneyCounter.Money + deposit.DepositValue;
        finalText.text = $"{finalScore}";
        string name = PlayerPrefs.GetString("CurrentPlayerName", "Player");
        LeaderboardManager.SaveScore(name, finalScore);
    }
}
