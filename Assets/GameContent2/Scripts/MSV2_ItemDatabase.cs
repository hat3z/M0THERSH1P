using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName= "ItemDatabase", menuName = "DEADLOCK/Create Item Database")]
public class MSV2_ItemDatabase : ScriptableObject
{
    [Header("Player Bullet Pools")]
    public int PlayerBaseBulletPool;

    [Header("Enemy Bullet Pools")]
    public int Tier3EnemyBulletPool;
    public int Tier4EnemyBulletPool;
    public int Tier5EnemyBulletPool;

    [Header("Enemy Damages by Tier")]
    public float Tier3EnemyProjectileDamage;
    public float Tier4EnemyProjectileDamage;
    public float Tier5EnemyProjectileDamage;

    [Header("Items")]
    public List<DropItem> Items;

    [Header("Enemy Startup Pool Options")]
    public EnemyStartupPoolTable EnemyPoolTable;

    [Header("Enemy Spawn Control by Stage")]
    public List<EnemySpawnTable> EnemySpawnTables;

    private const double TIER_1_PROBABILITY = 0.5f; // 50% chance
    private const double TIER_2_PROBABILITY = 0.4f; // 40% chance
    private const double TIER_3_PROBABILITY = 0.3f; // 30% chance
    private static System.Random random = new System.Random();
    public DropItem GetRandomItem()
    {
        double roll = random.NextDouble();
        if(roll < TIER_1_PROBABILITY)
        {
            // Tier 1 Loot
            int index = random.Next(GetItemListByTier(Item.itemRarity.Tier1).Count);
            return GetItemListByTier(Item.itemRarity.Tier1)[index];
        }
        else if(roll < TIER_1_PROBABILITY + TIER_2_PROBABILITY)
        {
            // Tier 2 Loot
            int index = random.Next(GetItemListByTier(Item.itemRarity.Tier2).Count);
            return GetItemListByTier(Item.itemRarity.Tier2)[index];
        }
        else
        {
            // Tier 3 Loot
            int index = random.Next(GetItemListByTier(Item.itemRarity.Tier3).Count);
            return GetItemListByTier(Item.itemRarity.Tier3)[index];
        }
    }

    public List<DropItem> GetItemListByTier(Item.itemRarity _rarity)
    {
        List<DropItem> result = new List<DropItem>();
        {
            foreach (DropItem item in Items)
            {
                if(item.ItemData.ItemRarity == _rarity)
                {
                    result.Add(item);
                }
            }
        }
        return result;

    }

    public int GetEnemyCountByType(MSV2_EnemyController.enemyType _type)
    {
        switch (_type)
        {
            case MSV2_EnemyController.enemyType.Tier1:
                return EnemyPoolTable.T1PoolCount;
            case MSV2_EnemyController.enemyType.Tier2:
                return EnemyPoolTable.T2PoolCount;
            case MSV2_EnemyController.enemyType.Tier3:
                return EnemyPoolTable.T3PoolCount;
            case MSV2_EnemyController.enemyType.Tier4:
                return EnemyPoolTable.T4PoolCount;
            case MSV2_EnemyController.enemyType.Tier5:
                return EnemyPoolTable.T5PoolCount;
        }
        return 0;
    }

    public EnemyStartupSpawnTable GetEnemyStartupPoolTable(MSV2_GameController.gameStage _stage)
    {
        switch (_stage)
        {
            case MSV2_GameController.gameStage.Stage1:
                return EnemySpawnTables[0].EnemyStartupTable;
            case MSV2_GameController.gameStage.Stage2:
                return EnemySpawnTables[1].EnemyStartupTable;
            case MSV2_GameController.gameStage.Stage3:
                return EnemySpawnTables[2].EnemyStartupTable;
        }
        return null;
    }

    #region -- ITEMS --
    [System.Serializable]
    public class Item
    {
        public enum itemRarity { Tier1, Tier2, Tier3, Tier2Upgrade, PlayerWeapon };
        public itemRarity ItemRarity;
        public enum itemType
        {
            AttackSpeeder, SpaceWars, AddSomeHealthPlease, IAmSpeed, AtomForTheBeasts, SlowDownBro,
            CircularHug, ReviveMe
        }
        public itemType ItemType;
    }

    [System.Serializable]
    public  class DropItem
    {
        public string Name;
        public Item ItemData;
        public Texture Icon;
        public string ItemDescription;


        public void AddItemToPlayer()
        {
            switch (ItemData.ItemType)
            {
                case Item.itemType.AttackSpeeder:
                    MSV2_WorldController.instance.GetActivePlayerModifiers().WP1_AttackSpeedMod += .15f;
                    break;
                case Item.itemType.SpaceWars:
                    MSV2_WorldController.instance.GetActivePlayerModifiers().WP1_DamageMod += .15f;
                    break;
                case Item.itemType.AddSomeHealthPlease:
                    MSV2_WorldController.instance.GetActivePlayerModifiers().healthAddMod += 15;
                    break;
                case Item.itemType.IAmSpeed:
                    MSV2_WorldController.instance.GetActivePlayerModifiers().moveSpeedMod += .5f;
                    break;
                case Item.itemType.AtomForTheBeasts:
                    break;
                case Item.itemType.SlowDownBro:
                    break;
                case Item.itemType.CircularHug:
                    break;
                default:
                    break;
            }
        }
    }
    #endregion -- ITEMS --

    #region -- PLAYER WEAPONS - ABILITIES --

    [System.Serializable]
    public class PlayerWeapon2SlotBase
    {
        public string Name;
        public string Description;
        public float AreaSize; // Plasma: Line height size | Circular: projectile size
        public float BaseDamage;
        public float CooldownTime;
    }

    [System.Serializable]
    public class PlayerAbilitySlot
    {
        public string Name;
        public string Description;
        public float BaseDamage;
        public float CooldownTime;
        public float AbilityTimeModifier;
        // Wormhole: lassulási idő
        // Cloaking: cloak lifetime
        // HoloProjection: clone lifetime
    }

    #endregion -- PLAYER WEAPONS - ABILITIES --

    #region -- ENEMY SPAWN COUNTS BY STAGES --
    [System.Serializable]
    public class EnemyStartupPoolTable
    {
        public int T1PoolCount;
        public int T2PoolCount;
        public int T3PoolCount;
        public int T4PoolCount;
        public int T5PoolCount;
    }
    [System.Serializable]
    public class EnemySpawnTable
    {
        public string DisplayName;
        public MSV2_GameController.gameStage GameStage;
        public EnemyStartupSpawnTable EnemyStartupTable;
        public int MaxT1EnemyCount;
        public int MaxT2EnemyCount;
        public int MaxT3EnemyCount;
        public int MaxT4EnemyCount;
        public int MaxT5EnemyCount;
    }
    [System.Serializable]
    public class EnemyStartupSpawnTable
    {
        public int T1StartupCount;
        public int T2StartupCount;
        public int T3StartupCount;
        public int T4StartupCount;
        public int T5StartupCount;
    }


    // Spawning Help:
    // Stage kezdetekor lespawnolunk mindegyik Tier-ből anniyt, amennyi az EnemyStartupTable-be van.
    // Ha a játékos öl, megnézzük mennyi T1 enemy van mondjuk, amennyi hiányzik a MaxT1EnemyCount-ból, annyit spawnololunk. A többi tiernél szintén.
    #endregion -- ENEMY SPAWN COUNTS BY STAGES --
}
