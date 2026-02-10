using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private int coolAmountOfSeconds = 10;
    [SerializeField] private TextMeshProUGUI timerText;

    private float startTime;
    private bool isFinished = false;
    private float finalTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (!isFinished)
        {
            float currentTime = Time.time - startTime;
            UpdateTimerDisplay(currentTime);
        }
    }

    public void OnWin()
    {
        if (isFinished) return;

        isFinished = true;
        finalTime = Time.time - startTime;

        DisplayFinalResult();
    }

    private void UpdateTimerDisplay(float time)
    {
        if (timerText != null)
        {
            timerText.text = $"Время: {time:F1} сек.";
        }
    }

    private void DisplayFinalResult()
    {
        if (timerText == null) return;

        if (finalTime <= coolAmountOfSeconds)
        {
            timerText.text = $"Финиш! {finalTime:F2} сек.\nТы крутой!";
            timerText.color = Color.green; 
        }
        else
        {
            timerText.text = $"Финиш! {finalTime:F2} сек.\nНе очень круто...";
            timerText.color = Color.red; 
        }
    }
}