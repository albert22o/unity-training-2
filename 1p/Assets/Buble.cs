using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Buble : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public int Score = 1;
    public bool Destroyed;
    public Action<int> OnScoreIncrease;

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
        StartCoroutine(Loose());
    }

    private IEnumerator Loose()
    {
        yield return new WaitForFixedUpdate();
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
