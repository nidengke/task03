using UnityEngine;
using System.Collections;

/// <summary>
/// 子弹
/// </summary>
public class Bullet : MonoBehaviour
{
    private float atk=1;
    private float moveSpeed = 100;

    [HideInInspector]
    public RaycastHit hit;
    private float atkDistance = 200;

    //public LayerMask layer;

    public Vector3 targetPos;
    //通过射线计算击中物体
    private void CalculateTargetPoint()
    {
        PlayerContral getLayer = new PlayerContral();
        if (Physics.Raycast(transform.position, transform.forward, out hit, atkDistance, getLayer.layer))
        {
            targetPos = hit.point;
        }
        else
        {
            targetPos = transform.TransformPoint(0, 0, atkDistance);
        }
    }

    private void Awake()
    {
        CalculateTargetPoint();
    }
    private void Update()
    {
        Movement();
        if ((transform.position - targetPos).sqrMagnitude < 0.1f)
        { //到达目标点
            //销毁子弹
            Destroy(gameObject);
            //生成特效
        }
        //根据击中部位（受击物体的名称 hit.collider.name），调用敌人受伤方法
        if (hit.collider != null && hit.collider.tag == "enemy")
        {
            Debug.Log("打中敌人了!!!!");
            AddScore();
            Debug.Log("目前分数"+score);
        }
    }
    public static int score=0;
    public void AddScore()
    {
        score++;
    }

    public Vector3 bulletPosition;

    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
        bulletPosition = transform.position;
    }

}
