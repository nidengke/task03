using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class AnimatorZombie : MonoBehaviour
{

    private Animator anim = null;
    public int number = 0;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    //private void Update()
    //{
    //    if (walkToAttack)
    //    {
    //    }
    //}

    private void AnimEventFunc()
    {
        print((number++).ToString());
    }

}
