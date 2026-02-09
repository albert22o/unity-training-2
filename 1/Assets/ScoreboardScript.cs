using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreboardScript : MonoBehaviour //скрипт, отвечающий за подсчёт очков
{
    [Header("References")]
    [SerializeField] TMP_Text scoreText;        //ссылка на текст для отображения информации о собранных объектах

    [Header("Events")]
    public UnityEvent OnCollectAll;             //событие, генерируемое когда собраны все обекты

    int collectablesCount = 0;                  //общее количество собираемых обектов
    int collected = 0;                          //количество собранных объектов

    void Start()
    {
        //поиск всех обектов имеющих скрипт собираемого объекта
        CollectableScript[] collectables = FindObjectsByType<CollectableScript>(FindObjectsSortMode.None);

        collectablesCount = collectables.Length;

        foreach (CollectableScript collectable in collectables) //назначение обработчика событий всем собираемым объектам
            collectable.OnPlayerEnter.AddListener(onCollect);

        updateScoreText(collected, collectablesCount);          //обновление информации о собранных объектах
    }

    public void onCollect()                     //метод, вызываемый при сборе объекта
    {
        if (collected >= collectablesCount) return; //если все объекты собраны

        collected++;
        updateScoreText(collected, collectablesCount);

        if (collected >= collectablesCount)
            OnCollectAll?.Invoke();
    }

    void updateScoreText(int collected, int collectablesCount) //метод обновления информации о собранных объектах
    {
        scoreText.text = "Обьектов собрано " + collected.ToString() + "/" + collectablesCount.ToString();
    }
}