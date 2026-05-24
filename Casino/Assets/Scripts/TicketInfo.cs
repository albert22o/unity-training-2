using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TicketInfo : MonoBehaviour
{
    public IReadOnlyList<CardChance> CardChances => cardChances;
    public string Difficulty
    {
        get => difficulty;
        set
        {
            difficulty = value;
            difficultyText.text = $"{difficulty} билет";
        }
    }
    public int SpinCost
    {
        get => spinCost; set
        {
            spinCost = value;
            spinCostText.text = $"Цена билета: {spinCost}";
        }
    }

    [SerializeField] TextMeshProUGUI difficultyText;
    [SerializeField] TextMeshProUGUI spinCostText;
    [SerializeField] Transform chancesContainer;
    [SerializeField] CardChance cardChancePrefab;
    [SerializeField] LuckLevel luckLevel;
    [SerializeField] PaymentLevel paymentLevel;
    [SerializeField] GameObject ticketBGPanel;
    [SerializeField] Casino casino;
    [SerializeField] TextMeshProUGUI chanceTitle;
    [SerializeField] GameObject spinButton;

    private int spinCost;
    private string difficulty;
    private readonly List<CardChance> cardChances = new();
    private TicketSettings previousSettings;

    private void Awake()
    {
        if (cardChancePrefab == null || chancesContainer == null || difficultyText == null)
            Debug.LogError("Проставь все зависимости!");
        luckLevel.Change += () => Init(previousSettings);
        paymentLevel.Change += () => Init(previousSettings);
    }

    public void Init(TicketSettings settings)
    {
        if (settings == null)
            return;

        ticketBGPanel.SetActive(true);
        casino.gameObject.SetActive(true);
        spinButton.SetActive(true);
        chanceTitle.text = "Шансы и награды:";

        previousSettings = settings;
        Difficulty = settings.Dfficulty;
        SpinCost = settings.SpinCost;

        foreach (var cardChance in cardChances)
        {
            Destroy(cardChance.gameObject);
        }
        cardChances.Clear();
        var modifiedChances = settings.Chances
            .Select(chance =>
            {
                float newReward = chance.reward * (paymentLevel.Payment * 0.5f + 0.5f);
                float newChance = newReward > 0
                    ? chance.chance * (luckLevel.Luck * 0.5f + 0.5f)
                    : chance.chance;
                return new CardChanceSetting { reward = newReward, chance = newChance, icon = chance.icon };
            })
            .ToList();
        var cumulativeSum = modifiedChances.Sum(chance => chance.chance); 
        foreach (var cardChanceSettings in modifiedChances)
        {
            var cardChance = Instantiate(cardChancePrefab, chancesContainer);
            cardChance.Init(cardChanceSettings, (int) (100 * cardChanceSettings.chance / cumulativeSum));
            cardChances.Add(cardChance);
        }
    }
}
