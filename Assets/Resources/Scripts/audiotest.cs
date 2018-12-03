using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///
///<summary>
public class audiotest : MonoBehaviour
{
    //音乐文件
    public AudioSource music;
    //音量
    public float musicVolume;

    void Start()
    {
        //设置默认音量
        musicVolume = 0.5F;
    }
    void OnGUI()
    {

        //播放音乐按钮
        if (GUI.Button(new Rect(10, 10, 100, 50), "Play music"))
        {

            //没有播放中
            if (!music.isPlaying)
            {
                //播放音乐
                music.Play();
            }

        }

    }
}
