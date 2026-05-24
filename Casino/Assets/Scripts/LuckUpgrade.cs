using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LuckUpgrade : MonoBehaviour
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

    [SerializeField] LuckLevel level;
    [SerializeField] MoneyCounter moneyCounter;

    [SerializeField] int upgradePrice = 10;
    [SerializeField] TextMeshProUGUI text;
    private Button button;

    void Awake()
    {
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
        level.Luck += 1;
        UpgradePrice *= 2;
    }
}
