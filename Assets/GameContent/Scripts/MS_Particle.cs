using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Particle : MonoBehaviour
{
    public enum type { Environment,EnviroExplosion ,Player, Enemy};
    public type Type;


    private void OnEnable()
    {
        StartCoroutine(DeactivateParticle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator DeactivateParticle()
    {
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<ParticleSystem>().Stop();
        gameObject.SetActive(false);
    }
}
