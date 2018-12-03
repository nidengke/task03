using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

///<summary>
///
///<summary>
[RequireComponent(typeof(EnemyStatusInfo))]
public class EnemyPathFinding : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Animator anim;

    Vector3 target;
    Vector3 north;
    Vector3 west;
    Vector3 east;
    Vector3 south;
    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = transform.position;
        north = target + new Vector3(0, 0, 5);
        west = target + new Vector3(0, 0, -5);
        east = target + new Vector3(5, 0, 0);
        south = target + new Vector3(-5, 0, 0);
    }

    // Update is called once per frame
    private int speedChangeTime = 10;
    private int t1 = 1;
    public float autoMoveSpeed;
    private bool patrolFlag = true;
    public static bool walkToAttack = false;
    public static bool attackToWalk = false;
    public static bool walkToDeath = false;
    public static bool attackToDeath = false;

    int loop = 0;
    void Update()
    {
        autoMoveSpeed = navMeshAgent.speed;
        //给敌人随时间增加提升速度
        if (Time.time > speedChangeTime)
        {
            navMeshAgent.speed += 0.5f;
            speedChangeTime += 10;
        }
        if (patrolFlag)
        {
            if (Time.time > t1)
            {
                Patrol(loop);
                loop = (loop + 1) % 4;
                t1 += 3;
            }
            if (Vector3.Distance(transform.position, PlayerContral.playerPosition) < 10f)
            patrolFlag = false;
        }
        else
            navMeshAgent.SetDestination(PlayerContral.playerPosition);
        if (Vector3.Distance(transform.position, PlayerContral.playerPosition) < 3f)
        {
            anim.SetBool("walkToAttack", true);
        }
        else
        {
            anim.SetBool("attackToWalk", true);
        }

    }

    private void Patrol(int i)
    {
        switch (i)
        {
            case 0:
                navMeshAgent.SetDestination(north);
                break;
            case 1:
                navMeshAgent.SetDestination(west);
                break;
            case 2:
                navMeshAgent.SetDestination(south);
                break;
            case 3:
                navMeshAgent.SetDestination(east);
                break;
        }

    }
}
