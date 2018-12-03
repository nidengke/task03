using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class MYTestScript : MonoBehaviour
{

    Animation _anim;
    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animation>();


    }
    private void Update()
    {
        if (_anim != null)
            _anim.Play("death");
    }

}
