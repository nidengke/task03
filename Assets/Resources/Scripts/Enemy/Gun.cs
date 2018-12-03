using UnityEngine;
using System.Collections;

/// <summary>
/// 枪
/// </summary>
public class Gun : MonoBehaviour
{
 
    public GameObject bulletPrefab;
    public AudioSource source;

    public void Firing()
    {
        //source.Play();
        Debug.Log("shoot");
        //GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;

    }
     

}
