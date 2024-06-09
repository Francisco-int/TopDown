using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public Transform player;
    public float spawnRadius = 10f;
    public float spawnInterval = 5f;
    public int enemiesKilled = 0;
    [SerializeField] Text winText;
    [SerializeField] Text pressRToRestart;
   [SerializeField] bool bossSpawned;
    private List<EnemyController> enemiesScene = new List<EnemyController>();
    bool spawnEnemies;

    void Start()
    {
        spawnEnemies = true;
        bossSpawned = false;
        winText.enabled = false;
        pressRToRestart.enabled = false;
        StartCoroutine(SpawnEnemies());
    }
    private void Update()
    {
        if (enemiesKilled == 5 && bossSpawned == false)
        {
            Debug.Log("boss");
            bossSpawned = true;
            spawnEnemies = false;
            enemiesScene.AddRange(FindObjectsOfType<EnemyController>());
            foreach (EnemyController enemy in enemiesScene)
            {
                if (enemy != null)
                {
                    Destroy(enemy.gameObject);
                }
            }
            Vector3 spawnPosition = player.position + new Vector3(Random.Range(3, 8), player.transform.position.y, Random.Range(3, 8)) * spawnRadius;
            Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        }
    }
    IEnumerator SpawnEnemies()
    {
        while (spawnEnemies)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector3 spawnPosition = player.position + new Vector3(Random.Range(3, 8), player.transform.position.y, Random.Range(3, 8)) * spawnRadius;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }


    public void AddEnemyKilled()
    {
        enemiesKilled++;
        
    }
}
