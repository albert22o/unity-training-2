using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Casino : MonoBehaviour
{
    [SerializeField] TicketInfo ticketInfo;
    [SerializeField] MoneyCounter moneyCounter;
    [SerializeField] YearsCounter yearsCounter;
    [SerializeField] TextMeshProUGUI rewardText;

    // Заменяем Image на SlotReveal
    [SerializeField] SlotReveal firstSlot;
    [SerializeField] SlotReveal secondSlot;
    [SerializeField] SlotReveal thirdSlot;

    [SerializeField] Animator animator;

    // Задержка между раскрытием слотов (драматический эффект)
    [SerializeField] private float delayBetweenSlots = 0.3f;

    private bool isSpinning = false;

    private void Awake()
    {
        if (ticketInfo == null || moneyCounter == null || yearsCounter == null)
            Debug.LogError("Не все зависимости прокинуты");
    }

    public void Spin()
    {
        if (isSpinning) return;
        if (ticketInfo.CardChances.Count < 3) return;
        if (moneyCounter.Money < ticketInfo.SpinCost) return;
        if (yearsCounter.Years <= 0) return;

        yearsCounter.DecreaseYear();
        moneyCounter.Money -= ticketInfo.SpinCost;

        // Закрываем все слоты заглушками
        firstSlot.Cover();
        secondSlot.Cover();
        thirdSlot.Cover();
        rewardText.text = "";

        // Определяем результат
        CardChance winChance = GetWinChance();
        bool isWin = winChance != null && winChance.Reward > 0;

        if (isWin)
        {
            StartCoroutine(RevealWin(winChance));
        }
        else
        {
            StartCoroutine(RevealLose());
        }
    }

    private CardChance GetWinChance()
    {
        var cumulativeSum = ticketInfo.CardChances.Sum(card => card.Chance);
        var randomNum = Random.Range(0, cumulativeSum);
        float sum = 0f;

        foreach (var card in ticketInfo.CardChances)
        {
            sum += card.Chance;
            if (sum >= randomNum)
                return card;
        }
        return null;
    }

    private IEnumerator RevealWin(CardChance winChance)
    {
        isSpinning = true;

        yield return StartCoroutine(firstSlot.RevealAnimated(winChance.Icon));
        yield return new WaitForSeconds(delayBetweenSlots);

        yield return StartCoroutine(secondSlot.RevealAnimated(winChance.Icon));
        yield return new WaitForSeconds(delayBetweenSlots);

        yield return StartCoroutine(thirdSlot.RevealAnimated(winChance.Icon));

        // Показываем награду только после раскрытия всех слотов
        moneyCounter.Money += (int)winChance.Reward;
        rewardText.text = $"+{winChance.Reward}";
        animator.Play("Out");

        isSpinning = false;
    }

    private IEnumerator RevealLose()
    {
        isSpinning = true;

        CardChance first, second, third;

        // Генерируем до тех пор, пока не получим "не все одинаковые"
        do
        {
            first = GetWinChance();
            second = GetWinChance();
            third = GetWinChance();
        }
        while (first.Icon == second.Icon && second.Icon == third.Icon);

        yield return StartCoroutine(firstSlot.RevealAnimated(first.Icon));
        yield return new WaitForSeconds(delayBetweenSlots);
        yield return StartCoroutine(secondSlot.RevealAnimated(second.Icon));
        yield return new WaitForSeconds(delayBetweenSlots);
        yield return StartCoroutine(thirdSlot.RevealAnimated(third.Icon));

        rewardText.text = "Вы проиграли";
        isSpinning = false;
    }
}