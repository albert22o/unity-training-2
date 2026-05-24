using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MoneyCounter : MonoBehaviour
{
    public int Money
    {
        get => money;
        set
        {
            money = value;
            text.text = $"{money}$";
        }
    }

    private int money = 100;
    private TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Money = money;
    }
}
