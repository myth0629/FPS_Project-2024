using UnityEngine;

public class Z_Attack : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트를 할당하기 위한 변수
    public float attackRange = 5f; // 공격 범위

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        // 플레이어의 위치를 찾습니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        // 플레이어와의 거리를 계산합니다.
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // 거리가 공격 범위 이내인지 확인합니다.
        if (distanceToPlayer <= attackRange)
        {
            // 공격 애니메이션 재생
            animator.SetBool("isAttacking", true);
        }
        else
        {
            // 공격 애니메이션 중지
            animator.SetBool("isAttacking", false);
        }
    }
}
