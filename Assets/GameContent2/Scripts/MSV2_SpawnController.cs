using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSV2_SpawnController : MonoBehaviour
{
    public bool CreatePoolOnly; // Creating only the Enemies pool

    [Header("T1_Enemy Settings")]
    public GameObject T1_EnemyPrefab;
    public Transform T1_EnemyPool;
    int T1_EnemySpawnCount = 5;
    public int maxT1Enemy;
    public int currentT1Enemy;
    private float T1_spawnInterval = 10f; // seconds

    [Header("T2_Enemy Settings")]
    public GameObject T2_EnemyPrefab;
    public Transform T2_EnemyPool;
    int T2_EnemySpawnCount = 2;
    public int maxT2Enemy;
    public int currentT2Enemy;
    private float T2_spawnInterval = 15f; // seconds


    // Start is called before the first frame update
    void Start()
    {
        Create_T1Pool();
        Create_T2Pool();
    }

    void Create_T1Pool()
    {
        for (int i = 0; i < maxT1Enemy; i++)
        {
            GameObject t1e = Instantiate(T1_EnemyPrefab);
            t1e.gameObject.SetActive(false);
            t1e.transform.SetParent(T1_EnemyPool);
        }
        if(!CreatePoolOnly)
        {
            StartCoroutine(Spawn_T1Enemies());
        }
    }

    void Create_T2Pool()
    {
        for (int i = 0; i < maxT2Enemy; i++)
        {
            GameObject t1e = Instantiate(T2_EnemyPrefab);
            t1e.gameObject.SetActive(false);
            t1e.transform.SetParent(T2_EnemyPool);
        }
        if (!CreatePoolOnly)
        {
            StartCoroutine(Spawn_T2Enemies());
        }
    }

    IEnumerator Spawn_T1Enemies()
    {
        List<MSV2_EnemyController> enemiesReadyToSpawn = new List<MSV2_EnemyController>(); // Ide gyűjtük azt az 5 enemy-t, amiket kiválasztottunk a spawn-hoz.
        while(true)
        {
            yield return new WaitForSeconds(T1_spawnInterval);
            if (currentT1Enemy != maxT1Enemy)
            {
                currentT1Enemy += GetEnemySpawnCountByPlayerLevel(T1_EnemySpawnCount);
                enemiesReadyToSpawn = GetEnemiesFromPool(T1_EnemyPool, GetEnemySpawnCountByPlayerLevel(T1_EnemySpawnCount));
                for (int i = 0; i < enemiesReadyToSpawn.Count; i++)
                {
                    enemiesReadyToSpawn[i].transform.position = MSV2_WorldController.instance.GetRandomPosInSpawnBound();
                    enemiesReadyToSpawn[i].gameObject.SetActive(true);
                    enemiesReadyToSpawn[i].SetupForSpawn();
                }
            }
        }
    }

    IEnumerator Spawn_T2Enemies()
    {
        List<MSV2_EnemyController> enemiesReadyToSpawn = new List<MSV2_EnemyController>();
        while (true)
        {
            yield return new WaitForSeconds(T2_spawnInterval);
            if (currentT2Enemy != maxT2Enemy)
            {
                currentT2Enemy += GetEnemySpawnCountByPlayerLevel(T2_EnemySpawnCount);
                enemiesReadyToSpawn = GetEnemiesFromPool(T2_EnemyPool, GetEnemySpawnCountByPlayerLevel(T2_EnemySpawnCount));
                for (int i = 0; i < enemiesReadyToSpawn.Count; i++)
                {
                    enemiesReadyToSpawn[i].transform.position = MSV2_WorldController.instance.GetRandomPosInSpawnBound();
                    enemiesReadyToSpawn[i].gameObject.SetActive(true);
                    enemiesReadyToSpawn[i].SetupForSpawn();
                }
            }
        }
    }

    List<MSV2_EnemyController> GetEnemiesFromPool(Transform _poolTransform, int _spawnCount)
    {
        List<MSV2_EnemyController> result = new List<MSV2_EnemyController>();
        MSV2_EnemyController ec;
        int spCount = _spawnCount;
        for (int i = 0; i < _poolTransform.childCount; i++)
        {
            if(spCount != 0)
            {
                ec = _poolTransform.GetChild(i).GetComponent<MSV2_EnemyController>();
                if (!ec.IsAlive())
                {
                    result.Add(ec);
                    spCount--;
                }
            }

        }
        return result;
    }

    int GetEnemySpawnCountByPlayerLevel(int _spawnCount)
    {
        int result = _spawnCount + MSV2_WorldController.instance.PlayerController.currentLevel;
        return result;
    }

}
