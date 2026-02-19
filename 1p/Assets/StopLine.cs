using UnityEngine;
using UnityEngine.Events;

public class StopLine : MonoBehaviour
{
    public UnityEvent stopEvent;
    [SerializeField] private float timeToStop = 1.0f; 

    private float timer = 0f;
    private bool isObjectInside = false;

    private void Update()
    {
        if (isObjectInside)
        {
            timer += Time.deltaTime;

            if (timer >= timeToStop)
            {
                stopEvent.Invoke();
                isObjectInside = false;
                timer = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Buble") || collision.TryGetComponent<Buble>(out _))
        {
            isObjectInside = true;
            timer = 0f; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Buble") || collision.TryGetComponent<Buble>(out _))
        {
            isObjectInside = false;
            timer = 0f; 
        }
    }
}