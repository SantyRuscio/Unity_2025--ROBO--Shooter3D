using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuraci�n de Spawn")]
    public GameObject enemyPrefab;
    public int maxEnemiesToSpawn = 10;
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 3f;

    [HideInInspector] public int enemiesSpawned = 0;
    [HideInInspector] public int enemiesKilled = 0;

    private bool isSpawning = false;

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < maxEnemiesToSpawn)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            EnemigoSpawneado enemyScript = newEnemy.AddComponent<EnemigoSpawneado>();
            enemyScript.SetSpawner(this);

            enemiesSpawned++;
        }
    }

    public void NotifyEnemyDeath()
    {
        enemiesKilled++;
        Debug.Log("Enemigo muerto. Total: " + enemiesKilled);
    }
}
