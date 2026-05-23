using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI),typeof(Button))]
public class PaymentUpgrade : MonoBehaviour
{
    public int UpgradePrice
    {
        get => upgradePrice;
        set
        {
            upgradePrice = value;
            text.text = $"{upgradePrice}$";
        }
    }

    [SerializeField] PaymentLevel level;
    [SerializeField] MoneyCounter moneyCounter;

    private int upgradePrice = 10;
    private TextMeshProUGUI text;
    private Button button;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
        if (level == null || moneyCounter == null)
            Debug.LogError("Проставь все зависимости!");
    }

    private void Start()
    {
        UpgradePrice = upgradePrice;
        button.onClick.AddListener(() => { OnClick(); });
    }

    private void OnClick()
    {
        if (moneyCounter.Money < UpgradePrice)
            return;
        moneyCounter.Money -= UpgradePrice;
        level.Payment += 1;
        UpgradePrice *= 2;
    }
}
