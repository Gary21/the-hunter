using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float spawnInterval;
    public float intervalMultiplier;

    private void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval * intervalMultiplier);
        var spawnPoint = getRandomTransform();
        GameObject newEnemy = Instantiate(enemy, new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0),
            Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }

    private Transform getRandomTransform()
    {
        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index];
    }
}