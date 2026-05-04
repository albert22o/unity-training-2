using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Salary : MonoBehaviour
{
    public int Amount
    {
        get => amount;
        set
        {
            amount = value;
            text.text = $"Зарплата: {amount}$";
        }
    }

    [SerializeField] int amount = 50;
    private TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Amount = amount;
    }
}
