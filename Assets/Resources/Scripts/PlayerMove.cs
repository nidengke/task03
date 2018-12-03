using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed = 10;

    public float rotateSpeed = 100;       //设置旋转的速度
    public float maxh = 10;               //设置提升的最高高度

    private void Update()
    {
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


    }
    /// <summary>
    /// 向前移动
    /// </summary>
    public void MovementForward(float moveSpeed)
    {
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }
    public void MovementRight(float moveSpeed)
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    }

}