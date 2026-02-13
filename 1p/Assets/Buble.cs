using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Buble : MonoBehaviour
{
    new Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    private bool wasDropped;
    public bool Destroyed { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Destroyed)
            return;
        if (collision.gameObject.CompareTag("Buble"))
        {
            collision.gameObject.GetComponent<Buble>().Destroyed = true;
            Destroy(collision.gameObject);
            transform.localScale *= 1.2f;
            Color current = spriteRenderer.color;
            spriteRenderer.color = new Color(current.r, current.g - 0.2f, current.b - 0.2f, current.a);
        }
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (wasDropped) 
            return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (Input.GetMouseButton(0))
            rigidbody.MovePosition(mousePosition);
    }

    private void OnMouseUp() 
    { 
        wasDropped = true;
        StartCoroutine(Loose());
    }

    private IEnumerator Loose()
    {
        yield return new WaitForEndOfFrame();
        rigidbody.constraints = RigidbodyConstraints2D.None;
    }
}
