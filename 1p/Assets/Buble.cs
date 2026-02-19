using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Buble : MonoBehaviour
{
    public bool WasDropped => wasDropped;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    private bool wasDropped;
    private bool dragging;

    public int Score = 1;
    public bool Destroyed;
    public Action<int> OnScoreIncrease;
    public Action OnDropped;



    private void OnMouseDown()
    {
        dragging = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Destroyed)
            return;
        if (collision.gameObject.CompareTag("Buble") && collision.gameObject.GetComponent<Buble>().Score == Score)
        {
            collision.gameObject.GetComponent<Buble>().Destroyed = true;
            Destroy(collision.gameObject);
            transform.localScale *= 1.2f;
            OnScoreIncrease?.Invoke(Score);
            Score *= 2;
            Color current = spriteRenderer.color;
            spriteRenderer.color = new Color(current.r, current.g - 0.2f, current.b - 0.2f, current.a);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!dragging)
            return;
        if (wasDropped) 
            return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (Input.GetMouseButton(0))
            rb.MovePosition(mousePosition);
    }

    private void OnMouseUp() 
    {
        if (wasDropped)
            return;
        OnDropped?.Invoke();
        dragging = false;
        StartCoroutine(Loose());
    }

    private IEnumerator Loose()
    {
        yield return new WaitForFixedUpdate();
        wasDropped = true;
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
