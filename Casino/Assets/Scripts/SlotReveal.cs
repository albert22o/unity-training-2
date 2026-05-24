using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotReveal : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Sprite coverSprite; // спрайт-заглушка ("?")
    [SerializeField] private float flipDuration = 0.15f; // секунды на полфлипа

    private void Awake()
    {
        if (slotImage == null)
            slotImage = GetComponent<Image>();
    }

    /// <summary>Сразу скрыть слот (показать заглушку)</summary>
    public void Cover()
    {
        StopAllCoroutines();
        slotImage.sprite = coverSprite;
        slotImage.transform.localScale = Vector3.one;
    }

    /// <summary>Анимированно раскрыть спрайт</summary>
    public IEnumerator RevealAnimated(Sprite targetSprite)
    {
        // Фаза 1: схлопываем по Y (1 → 0)
        yield return StartCoroutine(ScaleY(1f, 0f, flipDuration));

        // Меняем спрайт пока он "плоский"
        slotImage.sprite = targetSprite;

        // Фаза 2: раскрываем обратно (0 → 1)
        yield return StartCoroutine(ScaleY(0f, 1f, flipDuration));
    }

    private IEnumerator ScaleY(float from, float to, float duration)
    {
        float elapsed = 0f;
        Vector3 scale = slotImage.transform.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            // EaseInOut для плавности
            t = t * t * (3f - 2f * t);
            scale.y = Mathf.Lerp(from, to, t);
            slotImage.transform.localScale = scale;
            yield return null;
        }

        scale.y = to;
        slotImage.transform.localScale = scale;
    }
}