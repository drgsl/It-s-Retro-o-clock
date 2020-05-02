using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterStrikeSpawner : MonoBehaviour
{
    public GameObject[] Enemies;

    public Transform[] SpawnPoints;

    public static bool Spawning = false;

    public float spawnRate = 2f;
    int MaxEnemies = 10;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
    }

    void SpawnEnemy()
    {
        if (Spawning && transform.childCount <=MaxEnemies)
        {
            GameObject go = Instantiate(Enemies[Random.Range(0, Enemies.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);
            go.transform.parent = gameObject.transform;
        }


    }
}
