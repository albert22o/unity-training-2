using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] public float speed = 50f;

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}