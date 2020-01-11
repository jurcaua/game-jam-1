using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public GameObject enemyPrefab;
    public float spawnRate;
    public float enemySpeed;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > spawnRate)
        {
            GameObject createdEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            EnemyFollow enemyFollow = createdEnemy.GetComponent<EnemyFollow>();
            if (enemyFollow)
            {
                enemyFollow.followTarget = player;
                enemyFollow.speed = enemySpeed;
            }

            // Reset the time
            startTime = Time.time;
        }
    }
}
