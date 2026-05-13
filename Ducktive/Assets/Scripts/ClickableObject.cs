using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}