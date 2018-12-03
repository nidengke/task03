using System.Collections;
using System.Collections.Generic;
using System.IO;//引用此命名空间是用于数据的写入与读取
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

///<summary>
///
///<summary>
public class Result : MonoBehaviour
{
    GameObject[] scoreTextList;
    GameObject getScore;
    GameObject Restart;
    Text currentText;
    public int[] rankScoreArray;
    byte[] bytes;
    public static bool resultSaveFlag = true;

    private void Awake()
    {
        bytes = new byte[1000];
        rankScoreArray = new int[11];
        scoreTextList = new GameObject[10];
    }
    void Start()
    {
        for(int i =0;i<10;i++)
            scoreTextList[i] = GameObject.FindGameObjectWithTag("Score"+i);
        getScore = GameObject.FindGameObjectWithTag("getScore");
        Restart = GameObject.FindGameObjectWithTag("Restart");
        Restart.GetComponent<Button>().onClick.AddListener(OnClick);


    }
    void OnClick()
    {
        Debug.Log("restart");
        SceneManager.LoadScene("start");
    }

    private void Update()
    {
        if (PlayerContral.flag)
        { 
            Debug.Log("score is resive" + PlayerContral.score);
            Load();
            for (int j = 0; j < rankScoreArray.Length; j++)
                Debug.Log("数字"+j+"是:"+ rankScoreArray[j]);
            int loopNumber = 0;
            foreach (var scoreText in scoreTextList)
            {
                currentText= scoreText.GetComponent<Text>();
                currentText.text = rankScoreArray[loopNumber].ToString();
                loopNumber++;
            }
            Text thisTimeScore = getScore.GetComponent<Text>();
            thisTimeScore.text = PlayerContral.score.ToString();
            PlayerContral.flag = false;
            SaveData();
        }
    }

    public void Load()
    {
        rankScoreArray[10] = PlayerContral.score;
        for (int i=0;i<10;i++)
        {
            if (PlayerPrefs.HasKey("user" + i))
                rankScoreArray[i] = PlayerPrefs.GetInt("user" + i);
            else
                rankScoreArray[i] = 1;
        }
        Array.Sort(rankScoreArray);
        Array.Reverse(rankScoreArray);
    }

    public void SaveData()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt("user"+i, rankScoreArray[i]);
        }
    }

}






