using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 플레이어 위치
    public Transform player;
    // 생성할 적 프리팹
    public GameObject enemyPrefab;
    // 최소 생성 거리
    public float minSpawnDistance = 10f;
    // 최대 생성 거리
    public float maxSpawnDistance = 20f;
    // 생성되는 시간 간격
    public float spawnInterval = 5f; 

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // 지정된 시간마다 적을 스폰
    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("적 생성됨");
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float distance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);

        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
        Vector3 spawnPosition = player.position + offset;
        spawnPosition.y = player.position.y;

        return spawnPosition;
    }
}
