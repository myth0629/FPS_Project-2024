using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator; // Animator 컴포넌트를 할당하기 위한 변수
    public float attackRange = 2f; // 공격 범위
    public float attackCooldown = 2f; // 공격 쿨다운 시간
    private float lastAttackTime; // 마지막 공격 시간
    private NavMeshAgent navMeshAgent; // NavMeshAgent 변수

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트를 할당합니다.
    }

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

        // 플레이어를 따라갑니다.
        navMeshAgent.SetDestination(player.transform.position);

        // 거리가 공격 범위 이내인지 확인하고 쿨다운 시간이 지났는지 확인합니다.
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            // 공격 애니메이션 재생
            animator.SetBool("isAttacking", true);
            lastAttackTime = Time.time; // 마지막 공격 시간을 업데이트합니다.
        }
        else
        {
            // 공격 애니메이션 중지
            animator.SetBool("isAttacking", false);
        }
    }
}
