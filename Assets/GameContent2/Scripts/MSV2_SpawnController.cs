using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MSV2_SpawnController : MonoBehaviour
{
    public bool CreatePoolOnly; // Creating only the Enemies pool

    [Header("T1_Enemy Settings")]
    public List<EnemySpawnController> EnemySpawnControllers;

    public void Create_MainPool()
    {
        foreach (EnemySpawnController spawnController in EnemySpawnControllers)
        {
            spawnController.CreatePool();
        }
    }

    int GetEnemySpawnCountByPlayerLevel(int _spawnCount)
    {
        int result = _spawnCount + MSV2_WorldController.instance.PlayerController.currentLevel;
        return result;
    }


    [System.Serializable]
    public class EnemySpawnController
    {
        public string GroupName; // Only for display.
        public MSV2_EnemyController.enemyType Type;
        public bool CreatePoolOnStartup = false; // IF true we create this pool on startup.
        public MSV2_EnemyController Prefab; // The  enemy prefab
        public Transform Pool; // The pool where we storing all enemies in the world.
        private int SingleSpawnCount = 5; // How many enemies can spawn after Player kill
        private float SingleSpawnInternal = 5f; // seconds
        private int ActiveEnemyCount; // how many enemies active 
        private bool poolCreated = false; // The MAIN pool is created?
        private bool startupSpawnCreated = false; // If true, the pool is created.
        public void CreatePool()
        {
            if(MSV2_GameController.Instance.GameStage == MSV2_GameController.gameStage.Loading)
            {
                if(!poolCreated && CreatePoolOnStartup)
                {
                    float maxEnemyInPool = MSV2_WorldController.instance.ItemDatabase.GetEnemyCountByType(Type);
                    for (int i = 0; i < maxEnemyInPool; i++)
                    {
                        GameObject t1e = Instantiate(Prefab.gameObject);
                        t1e.GetComponent<MSV2_EnemyController>().StartupInit();
                        t1e.gameObject.SetActive(false);
                        t1e.transform.SetParent(Pool);
                    }
                    poolCreated = true;
                }

            }
        }

        public void Spawn_Startup()
        {
            MSV2_ItemDatabase.EnemyStartupSpawnTable esst = MSV2_WorldController.instance.ItemDatabase.GetEnemyStartupPoolTable(MSV2_GameController.Instance.GameStage);
            List<MSV2_EnemyController> enemiesReady = new List<MSV2_EnemyController>();
            List<MSV2_EnemyController> allEnemiesList = new List<MSV2_EnemyController>();
            if (MSV2_GameController.Instance.GameStage == MSV2_GameController.gameStage.Stage1)
            {
                if (!startupSpawnCreated && poolCreated)
                {
                    switch(Type)
                    {
                        case MSV2_EnemyController.enemyType.Tier1:
                            enemiesReady = GetEnemiesFromPool(esst.T1StartupCount);
                            allEnemiesList.AddRange(enemiesReady);
                            break;
                        case MSV2_EnemyController.enemyType.Tier2:
                            enemiesReady = GetEnemiesFromPool(esst.T2StartupCount);
                            allEnemiesList.AddRange(enemiesReady);
                            break;
                        case MSV2_EnemyController.enemyType.Tier3:
                            enemiesReady = GetEnemiesFromPool(esst.T3StartupCount);
                            allEnemiesList.AddRange(enemiesReady);
                            break;
                        case MSV2_EnemyController.enemyType.Tier4:
                            enemiesReady = GetEnemiesFromPool(esst.T4StartupCount);
                            allEnemiesList.AddRange(enemiesReady);
                            break;
                        case MSV2_EnemyController.enemyType.Tier5:
                            enemiesReady = GetEnemiesFromPool(esst.T5StartupCount);
                            allEnemiesList.AddRange(enemiesReady);
                            break;
                    }
                    for (int i = 0; i < allEnemiesList.Count; i++)
                    {
                        allEnemiesList[i].gameObject.SetActive(true);
                        allEnemiesList[i].transform.position = MSV2_WorldController.instance.GetRandomPosInSpawnBound();
                    }
                    startupSpawnCreated = true;
                }
            }
        }
        List<MSV2_EnemyController> GetEnemiesFromPool(int _spawnCount)
        {
            List<MSV2_EnemyController> result = new List<MSV2_EnemyController>();
            MSV2_EnemyController ec;
            int spCount = _spawnCount;
            for (int i = 0; i < Pool.childCount; i++)
            {
                if (spCount != 0)
                {
                    ec = Pool.GetChild(i).GetComponent<MSV2_EnemyController>();
                    if (!ec.IsAlive())
                    {
                        result.Add(ec);
                        spCount--;
                    }
                }

            }
            return result;
        }
        //void Spawn_Runtime()
        //{
        //    List<MSV2_EnemyController> enemiesReadyToSpawn = new List<MSV2_EnemyController>(); // Ide gyűjtük azt az 5 enemy-t, amiket kiválasztottunk a spawn-hoz.
        //    while (true)
        //    {
        //        yield return new WaitForSeconds(T1_spawnInterval);
        //        if (currentT1Enemy != maxT1Enemy)
        //        {
        //            currentT1Enemy += GetEnemySpawnCountByPlayerLevel(T1_EnemySpawnCount);
        //            enemiesReadyToSpawn = GetEnemiesFromPool(T1_EnemyPool, GetEnemySpawnCountByPlayerLevel(T1_EnemySpawnCount));
        //            for (int i = 0; i < enemiesReadyToSpawn.Count; i++)
        //            {
        //                enemiesReadyToSpawn[i].transform.position = MSV2_WorldController.instance.GetRandomPosInSpawnBound();
        //                enemiesReadyToSpawn[i].gameObject.SetActive(true);
        //                enemiesReadyToSpawn[i].SetupForSpawn();
        //            }
        //        }
        //    }
        //}

    }
}
