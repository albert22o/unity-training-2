using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private Transform container;   // вертикальный LayoutGroup
    [SerializeField] private GameObject rowPrefab;  // префаб строки с двумя Text

    private void Start()
    {
        List<LeaderboardManager.Entry> entries = LeaderboardManager.LoadAll();

        foreach (Transform child in container)
            Destroy(child.gameObject);

        for (int i = 0; i < entries.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, container);
            TextMeshProUGUI[] texts = row.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = $"{i + 1}. {entries[i].name}";
            texts[1].text = entries[i].score.ToString();
        }
    }
}