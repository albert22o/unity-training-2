using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Casino : MonoBehaviour
{
    [SerializeField] TicketInfo ticketInfo;
    [SerializeField] MoneyCounter moneyCounter;
    [SerializeField] YearsCounter yearsCounter;

    [SerializeField] TextMeshProUGUI rewardText;

    [SerializeField] Image firstSlot;
    [SerializeField] Image secondSlot;
    [SerializeField] Image thirdSlot;

    private void Awake()
    {
        if (ticketInfo == null || moneyCounter == null || yearsCounter == null)
            Debug.LogError("Не все зависимости прокинуты");
    }

    public void Spin()
    {
        if (ticketInfo.CardChances.Count < 3)
            return;
        if (moneyCounter.Money < ticketInfo.SpinCost)
            return;
        if (yearsCounter.Years <= 0)
            return;
        yearsCounter.DecreaseYear();
        moneyCounter.Money -= ticketInfo.SpinCost;
        var cumulativeSum = ticketInfo.CardChances.Sum(card => card.Chance);
        var randomNum = Random.Range(0, cumulativeSum);
        CardChance winChance = null;
        foreach(var card in ticketInfo.CardChances)
        {
            var sum = 0f;
            sum += card.Chance;
            if (sum >= randomNum)
                winChance = card;
        }
        if ((winChance != null ? winChance.Reward : 0) > 0)
        {
            SetWinCombo(winChance.Icon);
            moneyCounter.Money += (int)winChance.Reward;
            rewardText.text = $"Вы выиграли: {winChance.Reward}";
        }
        else
        {
            SetLoseCombo();
            rewardText.text = "Вы проиграли";
        }
    }

    private void SetWinCombo(Sprite winSprite)
    {
        firstSlot.sprite = winSprite;
        secondSlot.sprite = winSprite;
        thirdSlot.sprite = winSprite;
    }

    private void SetLoseCombo()
    {
        var chances = ticketInfo.CardChances.ToList();

        var first = chances[Random.Range(0, chances.Count)];
        firstSlot.sprite = first.Icon;
        chances.Remove(first);

        var second = chances[Random.Range(0, chances.Count)];
        secondSlot.sprite = second.Icon;
        chances.Remove(second);

        var third = chances[Random.Range(0, chances.Count)];
        thirdSlot.sprite = third.Icon;
        chances.Remove(third);
    }
}
