using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Vector2> spawnPositions;
    public float respawnTime = 5f;

    private void Start()
    {
        foreach (Vector2 pos in spawnPositions)
        {
            SpawnEnemy(pos);
        }
    }

    void SpawnEnemy(Vector2 position)
    {
        StartCoroutine(Respawn(position));
    }

    IEnumerator Respawn(Vector2 position)
    {
        yield return new WaitForSeconds(respawnTime);
        EnemyPool.Instance.GetEnemy(position);
    }
}
