using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            StartCoroutine(StartFalling());
    }

    private IEnumerator StartFalling()
    {
        yield return new WaitForSeconds(0.3f);

        while (true)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

            yield return null;
        }
    }
}