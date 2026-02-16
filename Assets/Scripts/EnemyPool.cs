using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    [SerializeField] private GameObject meleePrefab;
    [SerializeField] private int initialPoolSize = 10;

    private Queue<GameObject> poolQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject enemy = Instantiate(meleePrefab, transform);
            enemy.SetActive(false);
            poolQueue.Enqueue(enemy);
        }
    }

    public GameObject GetEnemy()
    {
        if (poolQueue.Count > 0)
        {
            GameObject enemy = poolQueue.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }

        GameObject newEnemy = Instantiate(meleePrefab, transform);
        newEnemy.SetActive(true);
        return newEnemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        poolQueue.Enqueue(enemy);
    }
}