using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_WorldItem : MonoBehaviour
{
    public enum itemType { Asteroid, Debris};
    public itemType ItemType;

    public int itemHealth;

    public List<GameObject> ImpactFX;

    public List<string> Loot;

    bool canSpawn = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            int hitDamage = collision.gameObject.GetComponent<MS_BulletController>().bulletDamage;
            if(itemHealth >= hitDamage)
            {
                itemHealth -= hitDamage;
            }
            if(itemHealth == hitDamage)
            {
                // BUGOS - majd raer
                //MS_ParticlePool.Instance.PlayEnvironmentExplosionParticle();
                gameObject.SetActive(false);
            }
            // BUGOS - majd raer
            //PlayImpactFX(collision);

            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "EnvironmentItemTag")
        {
            Debug.Log("yop");
            CanSpawn = false;
        }
    }

    public bool CanSpawn
    {
        get
        {
            return canSpawn;
        }
        set
        {
            canSpawn = value;
        }
    }

    void PlayImpactFX(Collision _col)
    {
        if(gameObject.activeSelf)
        {
            ContactPoint contact = _col.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            GameObject selectedImpact = MS_ParticlePool.Instance.GetRandomEnvironmentParticle();
            Debug.Log("selected " + selectedImpact.gameObject.name);
            selectedImpact.transform.rotation = rot;
            selectedImpact.transform.position = pos;
            selectedImpact.gameObject.SetActive(true);
            selectedImpact.GetComponent<ParticleSystem>().Play();
        }

    }


}
