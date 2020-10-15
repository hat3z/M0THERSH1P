using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Particle : MonoBehaviour
{
    public enum type { Impact,Environment,EnviroExplosion ,Player, Enemy};
    public type Type;

    public float LifeTime;

    private void OnEnable()
    {
        if(Type != type.Environment)
        {
            Destroy(gameObject, LifeTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
