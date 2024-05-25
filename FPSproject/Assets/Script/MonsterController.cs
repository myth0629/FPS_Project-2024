using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Transform player; // 플레이어 오브젝트
    public float attackRange = 3f;
    public float attackRate = 1.5f;
    public int attackDamage = 10;
    private float nextAttackTime = 0f;
    public LayerMask playerLayer;

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
        // Nav Mesh Agent 추적
        nmAgent.SetDestination(player.position);
        
        // 태그명이 BugFly면 애니메이션 다르게 하기
        if(gameObject.CompareTag("BugFly")){
            anim.SetBool("FlyAttack", false);
            anim.SetBool("isFly", true);
        } else{
            anim.SetBool("isAttack", false);
            anim.SetBool("isCrawl", true);
        }
    }

    void Attack()
    {
        // 태그명이 BugFly면 애니메이션 다르게 하기
        if(gameObject.CompareTag("BugFly")){
            anim.SetBool("isFly", false);
            anim.SetBool("FlyAttack", true);
        }else{
            anim.SetBool("isCrawl", false);
            anim.SetBool("isAttack", true);
        }
        // 적의 공격범위 내에 플레이어가 있으면
        Collider[] hitPlayers = Physics.OverlapSphere(transform.position, attackRange, playerLayer);
        foreach(Collider player in hitPlayers){
            PlayerHealth playerhp = player.GetComponent<PlayerHealth>();
            if(playerhp != null){
                playerhp.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log("Enemy HP : " + currentHealth);
        if(currentHealth <= 0) {
            Destroy(gameObject.GetComponent<CapsuleCollider>());
            Die();
        }
    }

    void Die(){
        nmAgent.isStopped = true;// 움직임 멈추기
        if(transform.CompareTag("BugFly")){
            anim.SetTrigger("FlyDeath");
        }else{
            anim.SetTrigger("Death");
        }
        Debug.Log("Enemy Died");
        Invoke("Destroy", 2f);
    }

    void Destroy(){
        Destroy(gameObject);
    }
}