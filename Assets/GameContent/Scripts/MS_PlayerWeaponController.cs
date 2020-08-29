using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_PlayerWeaponController : MonoBehaviour
{

    public Transform activeWeaponTransform;
    public GameObject weaponBullerPrefab;
    public float bulletForce;
    public Transform bulletPool;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(weaponBullerPrefab, activeWeaponTransform);
            bullet.transform.SetParent(bulletPool);
            bullet.transform.localScale = new Vector3(.2f, .2f, .2f);
            bullet.GetComponent<Rigidbody>().AddForce(-activeWeaponTransform.forward * bulletForce);
        }
    }
}
