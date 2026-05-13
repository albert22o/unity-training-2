using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class SpriteMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private UnityEvent onIdle;
    [SerializeField] private UnityEvent onWalk;

    private SpriteRenderer spriteRenderer;
    private bool isMoving = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");

        if (input != 0)
        {
            Vector2 direction = Vector2.right * input;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, speed * Time.deltaTime + 0.1f, wallLayer);

            if (hit.collider == null)
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }

            spriteRenderer.flipX = input < 0;

            if (!isMoving)
            {
                isMoving = true;
                onWalk?.Invoke();
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                onIdle?.Invoke();
            }
        }
    }
}