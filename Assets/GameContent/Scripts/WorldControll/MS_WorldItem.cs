using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_WorldItem : MonoBehaviour
{
    public enum itemType { AsteroidSmall,AsteroidBig, Debris};
    public itemType ItemType;

    public int itemHealth;

    [Header("Loot settings")]
    public float LootItemDropForce;
    public List<string> Loot;
    public int maxLootItemCount;


    public List<GameObject> ImpactFX;
    public MS_ParticleLibrary ParticleLibrary;

    bool canSpawn = true;
    Collider collider;

    private void Start()
    {
        GetLootStartup();
        SetupColliderByType();
    }

    void SetupColliderByType()
    {
        if(GetComponent<BoxCollider>())
        {
           collider = GetComponent<BoxCollider>();
        }
        else if(GetComponent<SphereCollider>())
        {
            collider = GetComponent<SphereCollider>();
        }
        else if(GetComponent<CapsuleCollider>())
        {
            collider = GetComponent<CapsuleCollider>();
        }
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
                StartCoroutine(DestroyAndDropLoot());
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

    #region --- LOOT ---

    void GetLootStartup()
    {
        int randomItemIndex;
        for (int i = 0; i < maxLootItemCount; i++)
        {

            randomItemIndex = Random.Range(0, ItemDatabase.Instance.GetItemListByTier(Item.itemTier.Normal, ItemType).Count);
            Loot.Add(ItemDatabase.Instance.GetItemListByTier(Item.itemTier.Normal, ItemType)[randomItemIndex].itemName);
        }
    }

    void DropLoot()
    {
        GameObject lootItem;
        collider.enabled = false;
        for (int i = 0; i < Loot.Count; i++)
        {
            lootItem = Instantiate(ItemDatabase.Instance.GetGameObjectFromItemName(Loot[i]));
            lootItem.transform.SetParent(MS_WorldController.Instance.LootItemPool, true);
            lootItem.transform.position = transform.position;
        }
    }

    IEnumerator DestroyAndDropLoot()
    {
        DropLoot();
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }

    #endregion
}
