using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_WorldItem : MonoBehaviour
{
    public enum itemType { AsteroidSmall,AsteroidBig, Debris};
    public itemType ItemType;

    public int itemHealth;

    public List<GameObject> ImpactFX;

    public List<string> Loot;

    bool canSpawn = true;

    public MS_ParticleLibrary ParticleLibrary;

    Transform ParticlePool;

    private void OnEnable()
    {
        SetParticleByType();
    }

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
                MS_ParticleController.Instance.PlayExplosionParticle(ParticleLibrary,collision ,0);
                gameObject.SetActive(false);
            }
            // BUGOS - majd raer
            MS_ParticleController.Instance.PlayRandomImpactParticle(ParticleLibrary, collision);

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

    void SetParticleByType()
    {
        ParticlePool = transform.GetChild(0).transform;
        if(ParticlePool !=null)
        {
            switch (ItemType)
            {
                case itemType.AsteroidSmall:
                    MS_ParticleController.Instance.PlayEnvironmentParticle(ParticleLibrary,ParticlePool,transform.position,0);
                    break;
                case itemType.AsteroidBig:
                    MS_ParticleController.Instance.PlayEnvironmentParticle(ParticleLibrary, ParticlePool, transform.position, 0);
                    break;
                case itemType.Debris:
                    //MS_ParticleController.Instance.PlayEnvironmentParticle(ParticleLibrary, ParticlePool, transform.position, 0);
                    break;
                default:
                    break;
            }
        }

    }

}
