using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int winScore = 10;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private List<Buble> buble;
    private int score = 0;

    private void Start()
    {
        scoreText.text = $"Score: {score}. Try gain {winScore}";
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
        scoreText.text = $"Score: {this.score}. Try gain {winScore}";
        if (this.score >= winScore)
            Win();
    }

    private void Win()
    {
        scoreText.text = $"You win! Final score: {score}";
        Time.timeScale = 0;
    }

    public void Lose()
    {
        scoreText.text = $"You lose! Final score: {score}";
        Time.timeScale = 0;
    }
}
