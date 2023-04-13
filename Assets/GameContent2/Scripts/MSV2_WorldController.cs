using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSV2_WorldController : MonoBehaviour
{
    public static MSV2_WorldController instance;
    public MSV2_PlayerController PlayerController;
    public MSV2_ItemDatabase ItemDatabase;
    [Header("-- Env / World Settings")]
    public int maxWorldX;
    public int minWorldX;
    public int maxWorldY;
    public int minWorldY;

    [Header("-- Env / Star generating")]
    public Transform Env_WorldStarParent;
    public GameObject starPrefab;
    private float starMinSize = .01f;
    private float starMaxSize = .05f;
    private float starPoolSize = 3000;

    [Header("-- System / Pooling")]
    public GameObject P_WP1BulletPrefab;
    public Transform Sys_PWP1BulletParent;
    List<GameObject> p_baseBullets = new List<GameObject>();

    [Space(5)]
    public GameObject Tier3EnemyBulletPrefab;
    public Transform Sys_Tier3EnemyBulletParent;
    List<GameObject> t3E_bulletsPool = new List<GameObject>();

    [Header("-- System / Spawning --")]
    public CircleCollider2D PlayerSpawnBound;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Env_GenerateStars();
        Sys_GenerateAllBulletsPool();
    }

    // SYS

    public MSV2_PlayerController GetActivePlayer()
    {
        return PlayerController;
    }
    public PlayerModifiers GetActivePlayerModifiers()
    {
        return PlayerController.PlayerModifiers;
    }

    void Sys_GenerateAllBulletsPool()
    {
        GameObject b;
        for (int i = 0; i < ItemDatabase.PlayerBaseBulletPool; i++)
        {
            b = Instantiate(P_WP1BulletPrefab);
            b.gameObject.SetActive(false);
            b.transform.SetParent(Sys_PWP1BulletParent);
            p_baseBullets.Add(b);
        }
        for (int i = 0; i < ItemDatabase.Tier3EnemyBulletPool; i++)
        {
            b = Instantiate(Tier3EnemyBulletPrefab);
            b.gameObject.SetActive(false);
            b.transform.SetParent(Sys_Tier3EnemyBulletParent);
            t3E_bulletsPool.Add(b);
        }
    }

    public GameObject Sys_GetPlayerBaseBullet()
    {
        for (int i = 0; i < p_baseBullets.Count; i++)
        {
            if(!p_baseBullets[i].activeSelf)
            {
                return p_baseBullets[i];
            }
        }
        return null;
    }

    public GameObject Sys_GetTier3EnemyBullet()
    {
        for (int i = 0; i < t3E_bulletsPool.Count; i++)
        {
            if(!t3E_bulletsPool[i].activeSelf)
            {
                return t3E_bulletsPool[i];
            }
        }
        return null;
    }

    // ENV
    void Env_GenerateStars()
    {
        GameObject _starPrefab;
        for (int i = 0; i < starPoolSize; i++)
        {
            _starPrefab = Instantiate(starPrefab);
            _starPrefab.transform.position = GetRandomPosition();
            _starPrefab.transform.localScale = GetRandomScale();
            _starPrefab.transform.SetParent(Env_WorldStarParent);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 result = new Vector3(Random.Range(minWorldX, maxWorldX), Random.Range(minWorldY, maxWorldY), 0);
        return result;
    }

    Vector3 GetRandomRotation()
    {
        Vector3 result = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        return result;
    }

    Vector3 GetRandomScale()
    {
        float randomSize = Random.Range(starMinSize, starMaxSize);
        Vector3 result = new Vector3(randomSize, randomSize, randomSize);
        return result;
    }

    // ENEMY SPAWNING BASED BY THE SPAWNINGBOUND


    public Vector3 GetRandomPosInSpawnBound()
    {
        Vector3 center = Vector3.zero;
        float radius = 45;
        float ang = Random.value * 360;
        Vector3 pos = Vector3.zero;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = 0;
        return pos;
    }

    // ENEMY SPAWNING BASED BY THE SPAWNINGBOUND

}
