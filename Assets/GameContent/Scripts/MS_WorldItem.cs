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
                gameObject.SetActive(false);
            }
            PlayImpactFX(collision);
            Destroy(collision.gameObject);
        }
    }

    void PlayImpactFX(Collision _col)
    {
        ContactPoint contact = _col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        GameObject selectedImpact = ImpactFX[Random.Range(0, ImpactFX.Count)];
        GameObject impactGO =  Instantiate(selectedImpact, pos, rot, MS_WorldController.Instance.ParticlePool);
        impactGO.GetComponent<ParticleSystem>().Play();
        Destroy(impactGO, 2);
    }

}
