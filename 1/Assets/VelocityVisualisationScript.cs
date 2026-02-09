using UnityEngine;

[RequireComponent(typeof(Rigidbody))]        //для работы требуется компонент Rigidbody
[RequireComponent(typeof(LineRenderer))]     //для работы требуется компонент LineRenderer
public class VelocityVisualisationScript : MonoBehaviour    //скрипт, отвечающий за визуализацию вектора скорости объекта
{
    LineRenderer lr;
    Rigidbody rb;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + rb.linearVelocity);
    }
}
