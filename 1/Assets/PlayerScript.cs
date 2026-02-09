using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private CameraScript cameraControl;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private float lookInput;
    private bool onGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Событие перемещения (WASD / Стик)
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Событие поворота (Мышь / Стик)
    private void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>().x;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        // Поворачиваем камеру каждый кадр для плавности
        if (cameraControl != null)
        {
            cameraControl.Rotate(lookInput);
        }
    }

    private void Move()
    {
        if (cameraControl == null) return;

        // Формируем вектор направления относительно взгляда камеры
        Vector3 rawDirection = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 moveDirection = cameraControl.GetDirection(rawDirection);

        rb.AddForce(moveDirection * speed, ForceMode.Force);
    }

    private void OnJump()
    {
        if (!onGround) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGround = false;
    }
}