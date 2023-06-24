using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float spawnInterval;
    public float intervalMultiplier;

    private void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval * intervalMultiplier);
        GameObject newEnemy = Instantiate(enemy, new Vector3(transform.position.x, transform.position.y, 0),
            Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}