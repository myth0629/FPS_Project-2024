using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    public int ammoAmount = 20; // 추가될 탄약의 양

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon")) //
        {
            // 플레이어의 총에 탄약 추가
            GunScript gunScript = other.GetComponent<GunScript>();
            if(gunScript != null)
            {
                gunScript.bulletsIHave += ammoAmount; // 탄약 추가
                Debug.Log("탄약 획득");
                Destroy(gameObject); // 아이템 파괴
            }
        }
    }
}