using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트
    public float attackRange = 2f;
    public float attackRate = 1f;
    public int attackDamage = 10;
    private float nextAttackTime = 0f;
    private NavMeshAgent agent;

    NavMeshAgent nmAgent;
    Animator anim;


    void Start()
    {
        // AI 사용
        nmAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 플레이어와의 거리
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceToPlayer <= attackRange){
            if(Time.time >= nextAttackTime){
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        else{
            ChasePlayer();
        }
    }

    void ChasePlayer() // 플레이어 추적
    {
        nmAgent.SetDestination(player.position);
        anim.SetBool("isAttack", false);
        anim.SetTrigger("isCrawl");
    }

    void Attack()
    {
        anim.SetBool("isAttack", true);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}