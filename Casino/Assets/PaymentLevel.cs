using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PaymentLevel : MonoBehaviour
{
    public event Action Change;
    public int Payment
    {
        get => payment;
        set
        {
            payment = value;
            text.text = $"Выплаты: {payment} ур.";
            Change?.Invoke();
        }
    }

    private int payment = 1;
    private TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Payment = payment;
    }
}
