using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_LootItem : MonoBehaviour
{

    public string ItemName;

    Rigidbody rigidBody;
    float itemDropForce = 20;
    bool dropCooldown;
    private void Awake()
    {
        if(rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(StartDrop());
    }

    // Update is called once per frame
    void Update()
    {
       if(!dropCooldown)
        {
            StopDrop();
        }
    }

    IEnumerator StartDrop()
    {
        dropCooldown = true;
        rigidBody.AddForce(transform.up * itemDropForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(2);
        dropCooldown = false;
    }

    void StopDrop()
    {
        rigidBody.isKinematic = true;
    }

}
