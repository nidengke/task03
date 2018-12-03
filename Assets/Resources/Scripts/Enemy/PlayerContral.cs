using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(ObjAnimation),typeof(EnemyStatusInfo))]

public class PlayerContral : MonoBehaviour
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum State
    { 
        /// <summary>
        /// 攻击状态
        /// </summary>
        Attack,
        /// <summary>
        /// 死亡状态
        /// </summary>
        Death,
        /// <summary>
        /// 寻路状态
        /// </summary>
        Pathfinding,       
        /// <summary>
        /// 闲置状态
        /// </summary>
        Idle

    }

    public static Vector3 playerPosition;

    private float atk = 1;
    private float bulletMoveSpeed = 100;

    [HideInInspector]
    public RaycastHit hit;
    private float atkDistance = 200;
    public LayerMask layer;

    public Vector3 targetPos;
    //通过射线计算击中物体
    private void CalculateTargetPoint()
    {
        if (Physics.Raycast(firePoint.position, transform.forward, out hit, atkDistance, layer))
        {
            targetPos = hit.point;
            if (hit.collider.tag == "enemy")
            {
                AddScore();
                Debug.Log("打中敌人了"+score);
                hit.collider.GetComponent<Transform>().position -= hit.normal;
                hit.collider.GetComponent<EnemyStatusInfo>().Injure();
                GenerateContactEffect();
            }
        }
        else
        {
            targetPos = firePoint.position + transform.forward * atkDistance;
        }
    }
    //生成接触粒子特效
    private void GenerateContactEffect()
    { // Resources/ContactEffects/xxxx
        //根据 受击物体标签（ hit.collider.tag） 创建相应特效
        //规定：读取的资源必须放置在 Resources 文件夹内
        //GameObject go = Resources.Load<GameObject>("ContactEffects/xx");
        Debug.Log("生成"+ hit.collider.tag +"特效");
        if (hit.collider == null) return;

        //特效命名规则：Effects+标签
        string prefabName = "ContactEffects/Effects" + hit.collider.tag;
        GameObject go = Resources.Load<GameObject>(prefabName);
        if (go)
        {
            Instantiate(go, targetPos + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal));
        }
    }


    private float moveSpeed = 8;
    private float rotateSpeed = 100;       //设置旋转的速度
    private ObjAnimation animAction;
    private Gun gun;
    private GunAnimation gunAnim;

    public int playerHP = 3;

    public AudioSource gunFireAudio;

    private float startTime, endTime;
    private void Start()
    {
        startTime = Time.time;
        animAction = GetComponent<ObjAnimation>();
        //motor = GetComponent<Motor>();
        gun = GetComponent<Gun>();
        gunAnim = GetComponent<GunAnimation>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="enemy")
        {
            Debug.Log("敌人碰撞到玩家了!");
            playerHP -= 1;
            if (playerHP <= 0)
                PlayerDeath();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "targetPoint")
            PlayerDeath();
    }

    public static int score;
    public static bool flag = false;

    private void PlayerDeath()
    {
        endTime = Time.time;
        //score = (int)(endTime-startTime);
        Debug.Log("玩家死了");
        flag = true;
        SceneManager.LoadScene("end");
    }
    public static void AddScore()
    {
        score ++;
    }

    /// <summary>
    /// 敌人状态
    /// </summary>
    public State state = State.Idle;

    private void Update()
    {
        playerPosition = this.transform.position;
        
        if (Input.GetMouseButton(1))
        {
            float lr = Input.GetAxis("Mouse X");//获取鼠标的左右上下偏移量
            this.transform.RotateAround(this.transform.position, Vector3.up, Time.deltaTime * rotateSpeed * lr);//每帧旋转空物体，相机也跟随旋转
            // float ud = Input.GetAxis("Mouse Y");
            //this.transform.RotateAround(this.transform.position, Vector3.right, Time.deltaTime * rotateSpeed * ud);//每帧旋转空物体，相机也跟随旋转
        }
        if (Input.GetKey(KeyCode.W))
            MovementForward(moveSpeed);
        if (Input.GetKey(KeyCode.S))
            MovementForward(-moveSpeed);
        if (Input.GetKey(KeyCode.A))
            MovementRight(-moveSpeed);
        if (Input.GetKey(KeyCode.D))
            MovementRight(moveSpeed);



        switch (state)
        {
            case State.Pathfinding:
                Pathfinding();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Idle:
                Idle();
                break;
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("分数:"+score);
    }

    private float atkTime=0;
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float atkInterval = 1;

    /// <summary>
    /// 攻击延迟时间
    /// </summary>
    public float delay = 0.3f;

    private void Attack()
    {
        animAction.Play(animAction.atkName);
        if (Input.GetMouseButtonDown(0))
            Shoot();
        //Invoke("Shoot", delay);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
            state = State.Pathfinding;
    }


    private void Pathfinding()
    {
        //播放跑步动画
        animAction.Play(animAction.runName);
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) & !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D)) state = State.Idle;
        //调用马达寻路功能  如果到达终点，修改状态为 state 攻击
        //if (!motor.Pathfinding()) state = State.Attack; 
        if (Input.GetMouseButtonDown(0)) state = State.Attack;
    }

    private void Idle()
    {
        //播放闲置动画
        animAction.Play(animAction.idleName);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
            state = State.Pathfinding;
        if (Input.GetMouseButton(0)) state = State.Attack;
    }

    public GameObject bulletPrefab;
    public Transform firePoint;
    private void Shoot()
    {
        Debug.Log("shoot");
        gunFireAudio.Play();
        Vector3 dir = transform.forward;
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.LookRotation(dir)) as GameObject;
        bulletGO.AddComponent<Bullet>();
        CalculateTargetPoint();
        //gun.Firing();
    }

    public void MovementForward(float moveSpeed)
    {
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }
    public void MovementRight(float moveSpeed)
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }
    public void MovementUp(float moveSpeed)
    {
        transform.position += new Vector3(0, 0.2f, 0);
    }

}
