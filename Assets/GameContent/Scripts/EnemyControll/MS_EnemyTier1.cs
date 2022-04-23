using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_EnemyTier1 : MonoBehaviour
{

    public Transform Player;
    public float moveSpeed;
    public float minDist;
    public float maxDist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        if(Vector3.Distance(transform.position, Player.position) >= minDist)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if(Vector3.Distance(transform.position, Player.position) <= maxDist)
        {
            //Shoot if can
        }
    }
}
