using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/// <summary>
/// 敌人状态信息类
/// </summary>
public class EnemyStatusInfo : MonoBehaviour
{
    Animator anim;
    private NavMeshAgent navMeshAgent;
    private void Start()
    {
        anim = this.GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    /// <summary>
    /// 当前血量
    /// </summary>
    private float currentHP=5;
    /// <summary>
    /// 最大血量
    /// </summary>
    private float maxHP=5;

    public void Injure()
    {
        //如果敌人已经死亡
        if (currentHP <= 0) return;

        Debug.Log("当前血量:"+currentHP);
        currentHP -= 1;


        if (currentHP <= 0)
            Death();
    }

    /// <summary>
    /// 死亡延迟时间
    /// </summary>
    public float deathDelay =10;

    /// <summary>
    /// 死亡
    /// </summary>
 
    public void Death()
    {
        //销毁当前游戏物体
        Destroy(this.gameObject, deathDelay);
        //播放动画
        Debug.Log("敌人死了!");
        anim.SetBool("attackToDeath", true);
        anim.SetBool("walkToDeath", true);
        navMeshAgent.speed = 0;
    }
}
