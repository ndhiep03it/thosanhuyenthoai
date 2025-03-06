using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    private Queue<GameObject> pool = new Queue<GameObject>();

    public static EnemyPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetEnemy(Vector2 position)
    {
        if (pool.Count > 0)
        {
            GameObject enemy = pool.Dequeue();
            enemy.transform.position = position;
            enemy.SetActive(true);
            return enemy;
        }
        return Instantiate(enemyPrefab, position, Quaternion.identity);
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
}
