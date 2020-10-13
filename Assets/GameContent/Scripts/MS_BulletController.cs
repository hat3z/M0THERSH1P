using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_BulletController : MonoBehaviour
{
    public int bulletDamage;
    public float bulletLifeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BulletLifeTimeControl());
    }


    IEnumerator BulletLifeTimeControl()
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(gameObject);
    }

}
