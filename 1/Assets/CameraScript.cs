using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform player;
    [SerializeField, Range(0.1f, 100f)] private float rotationSpeed = 20f;

    private float rotationAngle = 0f;
    private Vector3 offset;

    private void Start()
    {
        if (player != null)
            offset = transform.position - player.position;
    }

    // LateUpdate исключает дрожание камеры
    private void LateUpdate()
    {
        if (player == null) return;

        Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);
        transform.position = player.position + (rotation * offset);
        transform.LookAt(player.position);
    }

    public void Rotate(float delta)
    {
        rotationAngle += delta * rotationSpeed * Time.deltaTime;
    }

    public Vector3 GetDirection(Vector3 inputVector)
    {
        // Преобразуем локальный ввод относительно поворота камеры
        Vector3 direction = transform.TransformDirection(inputVector);
        direction.y = 0; // Убираем влияние наклона камеры на движение
        return direction.normalized;
    }
}