using System;
using System.Collections;
using UnityEngine;

public class BubleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bublePrefab;
    [SerializeField] private ScoreManager scoreManager;
    private void Start()
    {
        SpawnBuble();
    }

    private IEnumerator SpawnBubleLater()
    {
        yield return new WaitForSeconds(1);
        SpawnBuble();
    }

    private void SpawnBuble()
    {
        Vector3 spawnPosition = new(gameObject.transform.position.x, gameObject.transform.position.y);
        GameObject newBubleObj = Instantiate(bublePrefab, spawnPosition, Quaternion.identity);


        if (newBubleObj.TryGetComponent<Buble>(out Buble bubleScript))
        {
            bubleScript.OnDropped += () => StartCoroutine(SpawnBubleLater());
            bubleScript.OnScoreIncrease += scoreManager.IncreaseScore;
        }
    }
}
