using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardChance : MonoBehaviour
{
    public float Chance
    {
        get => chance;
        set
        {
            chance = value;
            chanceText.text = $"{chance}%";
        }
    }
    public Sprite Icon
    {
        get => icon;
        set
        {
            icon = value;
            imageIcon.sprite = icon;
        }
    }
    public float Reward
    {
        get => reward;
        set
        {
            reward = value;
            rewardText.text = $"+{reward}";
        }
    }

    [SerializeField] TextMeshProUGUI chanceText;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] Image imageIcon;

    private Sprite icon;
    private float chance;
    private float reward;

    public void Init(Sprite icon, float chance, float reward)
    {
        if (chanceText == null || rewardText == null || imageIcon == null)
            Debug.LogError("Подключи все зависимости");
        Chance = chance;
        Icon = icon;
        Reward = reward;
    }

    public void Init(CardChanceSetting cardChance, float relativeChance)
    {
        Init(cardChance.icon, relativeChance, cardChance.reward);
    }
}
