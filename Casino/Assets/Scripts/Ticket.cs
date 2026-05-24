using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Button))]
public class Ticket : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TicketSettings settings;
    [SerializeField] TicketInfo ticketInfo;

    [Header("Hover Animation")]
    [SerializeField] private float hoverOffsetY = 20f;      // насколько выдвигается вверх (в пикселях UI)
    [SerializeField] private float animationDuration = 0.2f; // скорость анимации

    private Button button;
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Coroutine currentAnimation;

    private void Awake()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;

        if (settings == null || ticketInfo == null)
            Debug.LogError("Не все зависимости проставлены");
    }

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AnimateTo(originalPosition + Vector2.up * hoverOffsetY);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AnimateTo(originalPosition);
    }

    private void AnimateTo(Vector2 targetPosition)
    {
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(MoveCoroutine(targetPosition));
    }

    private IEnumerator MoveCoroutine(Vector2 target)
    {
        Vector2 start = rectTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / animationDuration);
            rectTransform.anchoredPosition = Vector2.Lerp(start, target, t);
            yield return null;
        }

        rectTransform.anchoredPosition = target;
    }

    private void OnClick()
    {
        ticketInfo.Init(settings);
    }
}