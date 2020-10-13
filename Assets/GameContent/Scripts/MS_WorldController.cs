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
    public int B_AsteroidMinSeed;
    public int B_AsteroidMaxSeed;
    public float B_AsteroidMinSize;
    public float B_AsteroidMaxSize;
    public List<GameObject> B_AsteroidPrefabs;

    [Header("Small Asteroid Settings")]
    public Transform S_AsteroidPool;
    public int S_AsteroidMinSeed;
    public int S_AsteroidMaxSeed;
    public float S_AsteroidMinSize;
    public float S_AsteroidMaxSize;
    public List<GameObject> S_AsteroidPrefabs;

    [Header("Debris Settings")]
    public Transform DebrisPool;
    public int debrisMinSeed;
    public int debrisMaxSeed;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Debris Generation
    void GenerateDebris()
    {
        int maxDebrisCount = Random.Range(debrisMinSeed, debrisMaxSeed);
        Debug.Log("Max Debris Count: " + maxDebrisCount);
        for (int i = 0; i < maxDebrisCount; i++)
        {
            GameObject debrisGO = Instantiate(DebrisPrefabs[Random.Range(0, DebrisPrefabs.Count)]);
            debrisGO.transform.SetParent(DebrisPool, false);
            debrisGO.transform.position = GetRandomPosition();
        }
    }
    #endregion

    #region Big Asteroid Generation
    void GenerateAsteroid_Big()
    {
        int maxAsteroidCount = Random.Range(B_AsteroidMinSeed, B_AsteroidMaxSeed);
        Debug.Log("Max AsteroidBig Count: " + maxAsteroidCount);
        for (int i = 0; i < maxAsteroidCount; i++)
        {
            GameObject b_asteroidGO = Instantiate(B_AsteroidPrefabs[Random.Range(0, B_AsteroidPrefabs.Count)]);
            b_asteroidGO.transform.SetParent(B_AsteroidPool, false);
            b_asteroidGO.transform.localScale = GetRandomAsteroidScale_Big();
            b_asteroidGO.transform.position = GetRandomPosition();
            b_asteroidGO.transform.eulerAngles = GetRandomRotation();
        }
    }
    #endregion

    #region Small Asteroid Generation
    void GenerateAsteroid_Small()
    {
        int maxAsteroidCount = Random.Range(S_AsteroidMinSeed, S_AsteroidMaxSeed);
        Debug.Log("Max AsteroidBig Count: " + maxAsteroidCount);
        for (int i = 0; i < maxAsteroidCount; i++)
        {
            GameObject s_asteroidGO = Instantiate(S_AsteroidPrefabs[Random.Range(0, S_AsteroidPrefabs.Count)]);
            s_asteroidGO.transform.SetParent(S_AsteroidPool, false);
            s_asteroidGO.transform.localScale = GetRandomAsteroidScale_Small();
            s_asteroidGO.transform.position = GetRandomPosition();
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
