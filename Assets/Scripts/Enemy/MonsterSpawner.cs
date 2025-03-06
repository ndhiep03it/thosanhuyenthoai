using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Singleton;
    public GameObject enemyPrefab;
    private Queue<GameObject> pool = new Queue<GameObject>();
    public float respawnTime = 3f; // Thời gian chờ để spawn lại quái

    public List<Vector2> spawnPoints = new List<Vector2>(); // Danh sách vị trí spawn cố định
    private Dictionary<GameObject, int> monsterSpawnIndex = new Dictionary<GameObject, int>(); // Lưu index vị trí spawn của từng quái

    private void Awake()
    {
        Singleton = this;
    }

    public void SpawnNewMonster(int spawnIndex)
    {
        if (spawnIndex < 0 || spawnIndex >= spawnPoints.Count) return; // Kiểm tra index hợp lệ

        Vector2 spawnPosition = spawnPoints[spawnIndex];
        GameObject monster;

        if (pool.Count > 0)
        {
            monster = pool.Dequeue();
            monster.transform.position = spawnPosition;
            monster.SetActive(true);
        }
        else
        {
            monster = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

        // Gán lại spawnIndex cho quái dù lấy từ pool hay tạo mới
        EnemyController enemy = monster.GetComponent<EnemyController>();
        enemy.vitribandau = spawnIndex;

        // Lưu index vị trí spawn của quái
        monsterSpawnIndex[monster] = spawnIndex;

        // Khi quái chết, spawn lại đúng vị trí cũ theo index
        monster.GetComponent<EnemyController>().OnDeath += () =>
        {
            StartCoroutine(RespawnMonster(monster)); // Chạy Coroutine TRƯỚC KHI SetActive(false)
            ReturnToPool(monster);
        };

    }


    private IEnumerator RespawnMonster(GameObject monster)
    {
        yield return new WaitForSeconds(respawnTime); // Chờ trước khi spawn lại

        if (monsterSpawnIndex.ContainsKey(monster))
        {
            int spawnIndex = monsterSpawnIndex[monster]; // Lấy index spawn của quái
            SpawnNewMonster(spawnIndex); // Spawn lại đúng vị trí cũ
        }
    }


    public void ReturnToPool(GameObject monster)
    {
        monster.SetActive(false);
        pool.Enqueue(monster);
    }
}
