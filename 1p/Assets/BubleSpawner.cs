using System;
using System.Collections;
using UnityEngine;

public class BubleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bublePrefab;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private float spawnInterval = 1f;
    private void Start()
    {
        StartCoroutine(SpawnBubleLater());
    }

    private IEnumerator SpawnBubleLater()
    {
        yield return new WaitForSeconds(spawnInterval);
        SpawnBuble();
        StartCoroutine(SpawnBubleLater());
    }

    private void SpawnBuble()
    {
        Vector3 spawnPosition = new(gameObject.transform.position.x, gameObject.transform.position.y);
        GameObject newBubleObj = Instantiate(bublePrefab, spawnPosition, Quaternion.identity);

        if (newBubleObj.TryGetComponent(out Buble bubleScript))
        {
            bubleScript.OnScoreIncrease += scoreManager.IncreaseScore;
        }
    }
}
