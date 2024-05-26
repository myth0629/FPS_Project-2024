using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    public int ammoAmount = 10; // 추가될 탄약의 양

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌 발생: " + other.name + "총알 획득");
        if (other.CompareTag("Player")) // 충돌한 객체가 플레이어인지 확인
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); // 플레이어의 PlayerHealth 컴포넌트 가져오기
            if (playerHealth != null) // 플레이어의 PlayerHealth 컴포넌트가 존재할 때
            {
                // 플레이어의 총에 탄약 추가
                GunScript gunScript = other.GetComponent<GunScript>();
                if (gunScript != null)
                {
                    gunScript.bulletsIHave += ammoAmount; // 탄약 추가
                }

                Destroy(gameObject); // 아이템 파괴
            }
        }
    }
}