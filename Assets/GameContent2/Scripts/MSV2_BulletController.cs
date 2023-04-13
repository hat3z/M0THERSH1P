using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSV2_BulletController : MonoBehaviour
{
    public bool inDeadCooldown; // If hitted an enemy, we disable the bullet for 2 sec.
    float lifeTime = 2f;
    void Update()
    {
        if(gameObject.activeSelf)
        {
            if(lifeTime != 0)
            {
                lifeTime -= Time.deltaTime;
            }
            if(lifeTime <= 0)
            {
                gameObject.SetActive(false);
                lifeTime = 2f;
            }
        }
    }
}
