using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning Options")]
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public int limitOnWaveOne = 30;
    public int limitExpansionIndex=5;
    public int limitEnemiesOnScreen;
    GameObject GameManager;
    public GameObject enemyPrefab;
    [Header("Time Between Spawns")]
    public float waitTime;
    [SerializeField] float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        limitOnWaveOne=limitOnWaveOne+ (GameManager.GetComponent<GameManager>().wave*limitExpansionIndex);
    }

    // Update is called once per frame
    void Update()
    {

        if (waitTime == 0 && GameManager.GetComponent<GameManager>().enemyKilled <= limitOnWaveOne && GameManager.GetComponent<GameManager>().enemyOnScreen<limitEnemiesOnScreen)
        {
            SpawnEnemy();
        }
        else
        {
            if (Time.time > timer && GameManager.GetComponent<GameManager>().enemyKilled <= limitOnWaveOne && GameManager.GetComponent<GameManager>().enemyOnScreen < limitEnemiesOnScreen)
            {
                timer = Time.time + 1 / waitTime;
                SpawnEnemy();
            }
        }
        waitTime=waitTime* GameManager.GetComponent<GameManager>().waveIndex;
        if(Time.time > timer && GameManager.GetComponent<GameManager>().enemyKilled >= limitOnWaveOne)
        {
            GameManager.GetComponent<GameManager>().CompleteWave();
        }
    }
    public void SpawnEnemy()
    {
        int number = Random.Range(1, 3);
        Debug.Log(number);
        switch (number)
        {
            case 1:
                Instantiate(enemyPrefab, spawn1);
                break;
            case 2:
                Instantiate(enemyPrefab, spawn2);
                break;
            case 3:
                Instantiate(enemyPrefab, spawn3);
                break;
        }
    }
}
