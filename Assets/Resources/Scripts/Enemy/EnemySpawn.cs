using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 敌人生成器
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    /// <summary>
    /// 开始时敌人数量
    /// </summary>
    private int startCount;

    /// <summary>
    /// 敌人血量
    /// </summary>
    private int startCountBlood = 5 ;

    

    private Vector3 playerPosition;


    private void Start()
    {
        playerPosition = PlayerContral.playerPosition;
        //CalculateWayLines();
        startCount = transform.childCount;
        for (int i = 0; i < startCount; i++)
        {
            CreateEnemy(i);
        }
    }

    /// <summary>
    /// 敌人类型
    /// </summary>
    public GameObject enemyTypes;

    /// <summary>
    /// 生成一个敌人(一开始根据startCount生成敌人，敌人死亡时生成敌人)
    /// </summary>

    private void CreateEnemy(int i)
    {
        Transform child =  transform.GetChild(i);

        //创建一个敌人
        //GameObject enemyGO = Instantiate(敌人预制件, 第一个路点, Quaternion.identity) as GameObject;
        GameObject enemyGO = Instantiate(enemyTypes, child.position, Quaternion.LookRotation(playerPosition - transform.position)) as GameObject;
        enemyGO.AddComponent<EnemyPathFinding>();
    }
 
}

