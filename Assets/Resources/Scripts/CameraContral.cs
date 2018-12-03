using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class CameraContral : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    public float rotateSpeed = 5;
    //private void Start()
    //{
    //    player = GameObject.FindGameObjectWithTag("player");
    //    offset = transform.position - player.transform.position;
        
    //}
    //private void Update()
    //{
    //    float lr = Input.GetAxis("Mouse X");//获取鼠标的左右上下偏移量
    //    float ud = Input.GetAxis("Mouse Y");
    //    //Debug.Log(lr + "和" + ud);
    //    //transform.Rotate(Time.deltaTime * rotateSpeed * lr);//每帧旋转空物体，相机也跟随旋转
    //    transform.Rotate(0, Time.deltaTime * rotateSpeed * ud,0,Space.World);//每帧旋转空物体，相机也跟随旋转
    //}
    //private void LateUpdate()
    //{
    //    //transform.position = player.transform.position + offset;
    //}
}
