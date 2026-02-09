using UnityEngine;
using UnityEngine.Events;

public class CollectableScript : MonoBehaviour //скрипт, отвечающий за вращение собираемых объектов
{
    [Header("Parameters")]
    [Range(1f, 360f)]
    [SerializeField] float rotationSpeed = 70; //скорость вращения собираемого объекта

    [Header("Events")]
    public UnityEvent OnPlayerEnter;           //событие, генерируемое если произошло пересечение с объектом игрока

    private void Start()
    {
        transform.Rotate(transform.up, Random.Range(0, 180)); //случайный поворот на старте
    }

    void Update()
    {
        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime); //вращение каждый кадр
    }

    private void OnTriggerEnter(Collider other) //при пересечении
    {
        if (other.transform.CompareTag("Player")) //если обект, с которым произошло пересечение, имеет тэг "Игрок"
        {
            OnPlayerEnter?.Invoke();
            gameObject.SetActive(false);
        }
    }
}