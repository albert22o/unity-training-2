using UnityEngine;
using System.Collections.Generic;

public static class LeaderboardManager
{
    private const int MaxEntries = 10;
    private const string CountKey = "LB_Count";

    [System.Serializable]
    public class Entry
    {
        public string name;
        public int score;
    }

    public static void SaveScore(string playerName, int score)
    {
        List<Entry> entries = LoadAll();

        entries.Add(new Entry { name = playerName, score = score });
        entries.Sort((a, b) => b.score.CompareTo(a.score));

        if (entries.Count > MaxEntries)
            entries = entries.GetRange(0, MaxEntries);

        PlayerPrefs.SetInt(CountKey, entries.Count);
        for (int i = 0; i < entries.Count; i++)
        {
            PlayerPrefs.SetString($"LB_Name_{i}", entries[i].name);
            PlayerPrefs.SetInt($"LB_Score_{i}", entries[i].score);
        }
        PlayerPrefs.Save();
    }

    public static List<Entry> LoadAll()
    {
        var entries = new List<Entry>();
        int count = PlayerPrefs.GetInt(CountKey, 0);

        for (int i = 0; i < count; i++)
        {
            entries.Add(new Entry
            {
                name = PlayerPrefs.GetString($"LB_Name_{i}", "---"),
                score = PlayerPrefs.GetInt($"LB_Score_{i}", 0)
            });
        }
        return entries;
    }

    public static void Clear()
    {
        int count = PlayerPrefs.GetInt(CountKey, 0);
        for (int i = 0; i < count; i++)
        {
            PlayerPrefs.DeleteKey($"LB_Name_{i}");
            PlayerPrefs.DeleteKey($"LB_Score_{i}");
        }
        PlayerPrefs.DeleteKey(CountKey);
        PlayerPrefs.Save();
    }
}