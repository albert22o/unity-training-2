using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveAble : MonoBehaviour
{
    private bool dragging;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        dragging = true;
    }

    private void Update()
    {
        if (!dragging)
            return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (Input.GetMouseButton(0))
            rb.MovePosition(mousePosition);
    }
}
