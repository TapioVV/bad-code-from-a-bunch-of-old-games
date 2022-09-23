using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject plasticBag;
    [SerializeField] GameObject woodWall;

    [SerializeField] List<GameObject> _rocks;
    [SerializeField] List<GameObject> _topObstacles;

    public float Speed;
    [SerializeField] float _spawnTimer;
    float _spawnTimeR;

    int spawnCount;
    int spawnTreshold = 1;
    [SerializeField] float maxSpeed;
    [SerializeField] float startSpeed;
    [SerializeField] float speedUp;

    float score;
    public int intScore;
    public TMP_Text scoreText;
    public bool gameEnd;

    void Start()
    {
        Speed = startSpeed;
    }

    void Update()
    {
        if(gameEnd == false)
        {
            score += Speed * 2 * Time.deltaTime;
        }
        intScore = (int) score;

        scoreText.text = intScore.ToString();

        if (Speed < maxSpeed)
        {
            Speed += Time.deltaTime / speedUp;
        }

        if(_spawnTimer > 1)
        {
            if(spawnCount >= spawnTreshold)
            {
                _spawnTimer -= 0.4f;
                spawnCount = 0;
                spawnTreshold += 1;
            }
        }

        if(_spawnTimeR > 0)
        {
            _spawnTimeR -= Time.deltaTime;
        }

        else if(_spawnTimeR <= 0)
        {
            int randomObject = Random.Range(0, 15);
            if(IsBetween(randomObject, 0, 1))
            {
                InstantiatePlasticBag();
                InstantiateTopObstacle();
                InstantiateRock();
            }
            if(IsBetween(randomObject, 2, 3))
            {
                InstantiateRock();
            }
            if (IsBetween(randomObject, 3, 5))
            {
                InstantiateTopObstacle();
            }
            if (IsBetween(randomObject, 6, 7))
            {
                InstantiateWoodWall();
            }
            if (IsBetween(randomObject, 7, 9))
            {
                InstantiatePlasticBag();
            }
            if(randomObject == 10)
            {
                InstantiateTopObstacle();
                InstantiateRock();
            }
            if (IsBetween(randomObject, 11, 12))
            {
                InstantiateTopObstacle();
                InstantiatePlasticBag();
            }
            if (IsBetween(randomObject, 13, 14))
            {
                InstantiatePlasticBag();
                InstantiateRock();
            }

            spawnCount++;

            _spawnTimeR = _spawnTimer;
        }
    }
    
    void InstantiateWoodWall()
    {
        GameObject spawnedWoodWall = Instantiate(woodWall, new Vector2(12, 0), Quaternion.identity);
        Destroy(spawnedWoodWall, 8);
    }
    void InstantiateRock()
    {
        GameObject spawnedRock = Instantiate(_rocks[Random.Range(0, _rocks.Count)], new Vector2(Random.Range(14, 20), -3.2f), Quaternion.identity);
        Destroy(spawnedRock, 8);
    }
    void InstantiateTopObstacle()
    {
        GameObject topObstacle = Instantiate(_topObstacles[Random.Range(0, _topObstacles.Count)], new Vector2(Random.Range(14, 20), 3.3426f), Quaternion.identity);
        Destroy(topObstacle, 8);
    }
    void InstantiatePlasticBag()
    {
        GameObject spawnedPlasticBag = Instantiate(plasticBag, new Vector2(Random.Range(11, 15), Random.Range(0, 4)), Quaternion.identity);
        Destroy(spawnedPlasticBag, 8);
    }
        public bool IsBetween(double testValue, double bound1, double bound2)
        {
            if (bound1 > bound2)
                return testValue >= bound2 && testValue <= bound1;
            return testValue >= bound1 && testValue <= bound2;
        }
}
