using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healthAmount = 20; // 체력 회복량

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 충돌한 객체가 플레이어인지 확인
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null) // 플레이어의 체력을 회복
            {
                playerHealth.Heal(healthAmount);
                Debug.Log("Heal Player HP : " + playerHealth.currentHealth);
                Destroy(gameObject); // 아이템 파괴
            }
        }
    }
}