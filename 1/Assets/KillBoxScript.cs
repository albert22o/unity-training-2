using UnityEngine;
using UnityEngine.Events;

public class KillBoxScript : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;            //событие, генерируемое при попадании объекта с тэгом "Игрок" в триггер

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
            OnPlayerEnter?.Invoke();
    }
}