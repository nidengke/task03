using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary>
///
///<summary>
public class startGame : MonoBehaviour
{

    public Button start;
    public Button exit;

    void Start()
    {
        //anmi.Play();
        start.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("main");
        });
        exit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

}
