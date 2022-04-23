using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSV2_BulletController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.gameObject.tag == "EnemyTag")
        {
            gameObject.SetActive(false);
        }
    }
}
