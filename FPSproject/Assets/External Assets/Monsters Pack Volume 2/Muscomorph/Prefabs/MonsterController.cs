using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public Transform target;
    NavMeshAgent nmAgent;
    Animation anim;

    void Start()
    {
        // AI 사용
        nmAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // 플레이어 추척
        nmAgent.SetDestination(target.position);
    }
}