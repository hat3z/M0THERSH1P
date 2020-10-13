using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_ParticlePool : MonoBehaviour
{
    public static MS_ParticlePool Instance;
    Transform ParticlePoolTransform;

    [Header("Environment Particles")]
    public int EnvironmentParticleSize;
    public List<GameObject> EnviroParticles;
    List<GameObject> EnviroParticlePool = new List<GameObject>();

    [Header("Player Particles")]
    public int PlayerParticleSize;
    public List<GameObject> PlayerParticles;

    [Header("Enemy Particles")]
    public int EnemyParticleSize;
    public List<GameObject> EnemyParticles;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(ParticlePoolTransform == null)
        {
            ParticlePoolTransform = GameObject.Find("_ParticlePool").transform;
        }

        StartCoroutine(GeneratePoolStartup());

    }

    IEnumerator GeneratePoolStartup()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("<color=green> Particle Pool created!</color>");
        // ENVIRONMENT
        for (int i = 0; i < EnvironmentParticleSize; i++)
        {
            for (int a = 0; a < EnviroParticles.Count; a++)
            {
                GameObject particle = Instantiate(EnviroParticles[a]);
                particle.transform.SetParent(ParticlePoolTransform, false);
                particle.transform.localScale = Vector3.one;
                particle.gameObject.SetActive(false);
                EnviroParticlePool.Add(particle);
            }
        }
        // PLAYER
        for (int b = 0; b < PlayerParticleSize; b++)
        {
            for (int c = 0; c < PlayerParticles.Count; c++)
            {
                GameObject particle = Instantiate(PlayerParticles[c]);
                particle.transform.SetParent(ParticlePoolTransform, false);
                particle.transform.localScale = Vector3.one;
                particle.gameObject.SetActive(false);
            }
        }
        // ENEMY
        for (int d = 0; d < EnemyParticleSize; d++)
        {
            for (int e = 0; e < EnemyParticles.Count; e++)
            {
                GameObject particle = Instantiate(EnemyParticles[e]);
                particle.transform.SetParent(ParticlePoolTransform, false);
                particle.transform.localScale = Vector3.one;
                particle.gameObject.SetActive(false);
            }
        }
    }



    public GameObject GetRandomEnvironmentParticle()
    {
        return EnviroParticlePool[Random.Range(0, EnviroParticlePool.Count)];
    }

}
