using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_ParticleController : MonoBehaviour
{
    public static MS_ParticleController Instance;

    Transform ParticlePoolTransform;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        ParticlePoolTransform = GameObject.Find("_ParticlePool").transform;

    }

    #region --- IMPACT ---

    public void PlayRandomImpactParticle(MS_ParticleLibrary _particleLib, Collision _col)
    {
        GameObject particle = Instantiate(_particleLib.ImpactParticles[Random.Range(0, _particleLib.ImpactParticles.Count)]);
        particle.transform.SetParent(ParticlePoolTransform, false);
        particle.transform.localScale = Vector3.one;
        ContactPoint contact = _col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        particle.transform.position = pos;
        particle.transform.rotation = rot;
        particle.GetComponent<ParticleSystem>().Play();
    }

    public void PlayImpactParticle(MS_ParticleLibrary _particleLib, int _index, Collision _col)
    {
        GameObject particle = Instantiate(_particleLib.ImpactParticles[_index]);
        particle.transform.SetParent(ParticlePoolTransform, false);
        particle.transform.localScale = Vector3.one;
        ContactPoint contact = _col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        particle.transform.position = pos;
        particle.transform.rotation = rot;
        particle.GetComponent<ParticleSystem>().Play();
    }
    #endregion

    #region --- EXPLOSION ---
    public void PlayExplosionParticle(MS_ParticleLibrary _particleLib, Collision _col)
    {
        GameObject particle = Instantiate(_particleLib.ExplosionParticles[Random.Range(0, _particleLib.ExplosionParticles.Count)]);
        particle.transform.SetParent(ParticlePoolTransform, false);
        particle.transform.localScale = Vector3.one;
        ContactPoint contact = _col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        particle.transform.position = pos;
        particle.transform.rotation = rot;
        particle.GetComponent<ParticleSystem>().Play();
    }

    public void PlayExplosionParticle(MS_ParticleLibrary _particleLib, Transform _origin, int _index)
    {
        GameObject particle = Instantiate(_particleLib.ExplosionParticles[_index]);
        particle.transform.SetParent(ParticlePoolTransform, false);
        particle.transform.localScale = Vector3.one;
        particle.transform.position = _origin.position;
        particle.GetComponent<ParticleSystem>().Play();
    }

    public void PlayExplosionParticle(MS_ParticleLibrary _particleLib,Collision _col ,int _index)
    {
        GameObject particle = Instantiate(_particleLib.ExplosionParticles[_index]);
        particle.transform.SetParent(ParticlePoolTransform, false);
        particle.transform.localScale = Vector3.one;
        ContactPoint contact = _col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        particle.transform.position = pos;
        particle.transform.rotation = rot;
        particle.GetComponent<ParticleSystem>().Play();
    }
    #endregion

    #region --- MISC ---
    public void PlayRandomMiscParticle(MS_ParticleLibrary _particleLib, Collision _col)
    {
        GameObject particle = Instantiate(_particleLib.MiscParticles[Random.Range(0, _particleLib.MiscParticles.Count)]);
        particle.transform.SetParent(ParticlePoolTransform, false);
        particle.transform.localScale = Vector3.one;
        ContactPoint contact = _col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        particle.transform.position = pos;
        particle.transform.rotation = rot;
        particle.GetComponent<ParticleSystem>().Play();
    }

    public void PlayMiscParticle(MS_ParticleLibrary _particleLib, Transform _origin, int _index)
    {
        GameObject particle = Instantiate(_particleLib.MiscParticles[_index]);
        particle.transform.SetParent(ParticlePoolTransform, false);
        particle.transform.localScale = Vector3.one;
        particle.transform.position = _origin.position;
        particle.GetComponent<ParticleSystem>().Play();
    }

    public void PlayMiscParticle(MS_ParticleLibrary _particleLib, Collision _col ,int _index)
    {
        GameObject particle = Instantiate(_particleLib.MiscParticles[_index]);
        particle.transform.SetParent(ParticlePoolTransform, false);
        particle.transform.localScale = Vector3.one;
        ContactPoint contact = _col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        particle.transform.position = pos;
        particle.transform.rotation = rot;
        particle.GetComponent<ParticleSystem>().Play();
    }
    #endregion

    #region --- ENVIRONMENT ---
    public void PlayEnvironmentParticle(MS_ParticleLibrary _particleLib, Vector3 _origin,int _index)
    {
        GameObject particle = Instantiate(_particleLib.EnvironmentParticles[_index]);
        particle.transform.SetParent(ParticlePoolTransform, false);
        particle.transform.localScale = Vector3.one;
        particle.transform.position = _origin;
        particle.GetComponent<ParticleSystem>().Play();
    }
    public void PlayEnvironmentParticle(MS_ParticleLibrary _particleLib, Transform _poolParent, Vector3 _origin ,int _index)
    {
        GameObject particle = Instantiate(_particleLib.EnvironmentParticles[_index]);
        particle.transform.SetParent(_poolParent, false);
        particle.transform.localScale = Vector3.one;
        particle.transform.position = _origin;
        particle.GetComponent<ParticleSystem>().Play();
    }

    #endregion
}
