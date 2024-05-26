using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    
    public GameObject healthItemPrefab; // HealthItem 프리팹
    public GameObject ammoItemPrefab; // AmmoItem 프리팹

    private void Start()
    {
        // Dummie의 죽음 이벤트에 Subscribe하여 죽음 이벤트 발생 시 아이템을 생성하도록 합니다.
        MonsterController enemy = GetComponent<MonsterController>();
        if (enemy != null)
        {
            enemy.OnDeath += SpawnRandomItem;
        }
    }

    private void SpawnRandomItem()
    {
            Debug.Log("SpawnRandomItem 호출됨");

        // 랜덤으로 아이템을 선택하여 생성합니다.
        GameObject itemPrefab = Random.Range(0, 2) == 0 ? healthItemPrefab : ammoItemPrefab;
        if (itemPrefab != null)
        {
            Vector3 dummiePosition = transform.position; // Dummie의 위치를 가져옵니다.
            Instantiate(itemPrefab, dummiePosition, Quaternion.identity);
        }
    }
}
