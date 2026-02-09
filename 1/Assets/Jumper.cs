using UnityEngine;

public class Jumper : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out var rigidbody))
            rigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);
    }
}
