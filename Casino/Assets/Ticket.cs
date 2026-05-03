using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Ticket : MonoBehaviour
{
    [SerializeField] TicketSettings settings;
    [SerializeField] TicketInfo ticketInfo;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (settings == null || ticketInfo == null)
            Debug.LogError("Не все зависимости проставлены");
    }

    private void Start()
    {
        button.onClick.AddListener(() => { OnClick(); });
    }

    private void OnClick()
    {
        ticketInfo.Init(settings);
    }
}
