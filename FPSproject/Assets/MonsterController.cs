using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Transform player; // 플레이어 오브젝트
    public float attackRange = 2f;
    public float attackRate = 1f;
    public int attackDamage = 10;
    private float nextAttackTime = 0f;

    NavMeshAgent nmAgent;
    Animator anim;

    void Start()
    {
        
        // AI 사용
        nmAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
        // 체력
        currentHealth = maxHealth;
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
        anim.SetBool("isCrawl", true);
    }

    void Attack()
    {
        Debug.Log("Attacked!");
        anim.SetBool("isCrawl", false);
        anim.SetBool("isAttack", true);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        anim.SetTrigger("GetHit");
        Debug.Log("Enemy HP : " + currentHealth);
        if(currentHealth <= 0) {
            Destroy(gameObject.GetComponent<CapsuleCollider>());
            Die();
        }
    }

    void Die(){
        nmAgent.isStopped = true;// 움직임 멈추기
        anim.SetTrigger("Death");
        Debug.Log("Enemy Died");
        Invoke("Destroy", 2f);
    }

    void Destroy(){
        Destroy(gameObject);
    }
}