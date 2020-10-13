using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_WorldController : MonoBehaviour
{
    public static MS_WorldController Instance;

    [Header("World Settings")]
    public int maxWorldX;
    public int minWorldX;
    public int maxWorldY;
    public int minWorldY;

    [Header("Big Asteroid Settings")]
    public Transform B_AsteroidPool;
    public int B_AsteroidPoolSize;
    public float B_AsteroidMinSize;
    public float B_AsteroidMaxSize;
    public List<GameObject> B_AsteroidPrefabs;

    [Header("Small Asteroid Settings")]
    public Transform S_AsteroidPool;
    public int S_AsteroidPoolSize;
    public float S_AsteroidMinSize;
    public float S_AsteroidMaxSize;
    public List<GameObject> S_AsteroidPrefabs;

    [Header("Debris Settings")]
    public Transform DebrisPool;
    public int DebrisPoolSize;
    public List<GameObject> DebrisPrefabs;

    [Header("Particle Settings")]
    public Transform ParticlePool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateDebris();
        GenerateAsteroid_Small();
        GenerateAsteroid_Big();
    }


    #region Debris Generation
    void GenerateDebris()
    {
        MS_WorldItem worldItem;
        for (int i = 0; i < DebrisPoolSize; i++)
        {
            GameObject debrisGO = Instantiate(DebrisPrefabs[Random.Range(0, DebrisPrefabs.Count)]);
            worldItem = debrisGO.GetComponent<MS_WorldItem>();
            debrisGO.transform.SetParent(DebrisPool, false);
            if(worldItem.CanSpawn)
            {
                debrisGO.transform.position = GetRandomPosition();
            }

        }
    }
    #endregion

    #region Big Asteroid Generation
    void GenerateAsteroid_Big()
    {
        MS_WorldItem worldItem;
        for (int i = 0; i < B_AsteroidPoolSize; i++)
        {
            GameObject b_asteroidGO = Instantiate(B_AsteroidPrefabs[Random.Range(0, B_AsteroidPrefabs.Count)]);
            worldItem = b_asteroidGO.GetComponent<MS_WorldItem>();
            b_asteroidGO.transform.SetParent(B_AsteroidPool, false);
            b_asteroidGO.transform.localScale = GetRandomAsteroidScale_Big();
            if(worldItem.CanSpawn)
            {
                b_asteroidGO.transform.position = GetRandomPosition();
            }

            b_asteroidGO.transform.eulerAngles = GetRandomRotation();
        }
    }
    #endregion

    #region Small Asteroid Generation
    void GenerateAsteroid_Small()
    {
        MS_WorldItem worldItem;
        for (int i = 0; i < S_AsteroidPoolSize; i++)
        {
            GameObject s_asteroidGO = Instantiate(S_AsteroidPrefabs[Random.Range(0, S_AsteroidPrefabs.Count)]);
            worldItem = s_asteroidGO.GetComponent<MS_WorldItem>();
            s_asteroidGO.transform.SetParent(S_AsteroidPool, false);
            s_asteroidGO.transform.localScale = GetRandomAsteroidScale_Small();
            if(worldItem.CanSpawn)
            {
                s_asteroidGO.transform.position = GetRandomPosition();
            }
            s_asteroidGO.transform.eulerAngles = GetRandomRotation();
        }
    }
    #endregion

    #region Random Vectors / Scales / Rotations
    Vector3 GetRandomPosition()
    {
        Vector3 result = new Vector3(Random.Range(minWorldX, maxWorldX), Random.Range(minWorldY, maxWorldY), 0);
        return result;
    }

    Vector3 GetRandomAsteroidScale_Big()
    {
        float randomSize = Random.Range(B_AsteroidMinSize, B_AsteroidMaxSize);
        Vector3 result = new Vector3(randomSize, randomSize, randomSize);
        return result;
    }

    Vector3 GetRandomAsteroidScale_Small()
    {
        float randomSize = Random.Range(S_AsteroidMinSize, S_AsteroidMaxSize);
        Vector3 result = new Vector3(randomSize, randomSize, randomSize);
        return result;
    }

    Vector3 GetRandomRotation()
    {
        Vector3 result = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        return result;
    }

    #endregion
}
