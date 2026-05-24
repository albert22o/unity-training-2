using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillMoxUpgrade : MonoBehaviour
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

    [SerializeField] Salary level;
    [SerializeField] MoneyCounter moneyCounter;
    [SerializeField] TextMeshProUGUI text;

    private int upgradePrice = 300;
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
        level.Amount += 1;
        UpgradePrice *= 2;
    }
}
