using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] BoxCollider2D[] spawnAreas;
    [SerializeField] BoxCollider2D spawnArea;

    [SerializeField] BoxCollider2D movementBox;

    [SerializeField] Enemy enemyPrefab;

    int[] enemyHolder;

    int enemyAmountMin;
    int enemyAmountMax;
    float spawnTimerMin;
    float spawnTimerMax;

    float spawnTimer;
    float spawnTimerR;

    public bool activated;

    int myId;

    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        enemyAmountMin = gm.eAmountMin;
        enemyAmountMax = gm.eAmountMax;

        spawnTimerMin = gm.eSpawnTimerMin;
        spawnTimerMax = gm.eSpawnTimerMax;
        
        if(activated == true)
        {
            if (spawnTimerR > 0)
            {
                spawnTimerR -= Time.deltaTime;
            }
            if (spawnTimerR <= 0)
            {
                SpawnEnemies();
                spawnTimer = Random.Range(spawnTimerMin, spawnTimerMax);
                spawnTimerR = spawnTimer;
            }
        }
        if(activated == false)
        {
            Enemy[] enemiesLeft = FindObjectsOfType<Enemy>();
            foreach(Enemy enemyLeft in enemiesLeft)
            {
                enemyLeft.StopAllTweens();
                Destroy(enemyLeft.gameObject);
            }
        }
    }
    void SpawnEnemies()
    {
        int randomAmount = Random.Range(enemyAmountMin, enemyAmountMax);
        enemyHolder = new int[randomAmount];

        foreach(int enemyI in enemyHolder)
        {
            spawnArea = spawnAreas[Random.Range(0, 4)];
            Vector2 spawnLocation = new Vector2(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));
            Enemy enemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
            myId += 1;
            enemy.myId = myId;
            enemy.moveArea = movementBox;
        }
    }
}
