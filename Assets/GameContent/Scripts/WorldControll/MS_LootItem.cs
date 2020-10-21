using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_LootItem : MonoBehaviour
{

    public string ItemName;

    Rigidbody rigidBody;
    float itemDropForce = 2;
    float itemDropForceCounter;
    bool dropCooldown;

    List<float> dropAngles = new List<float>()
    {30, 60,90,120, 150,180,210,240,270,300,330};

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
        itemDropForceCounter = itemDropForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(!dropCooldown)
        {
            if (rigidBody.drag != 25)
            {
                rigidBody.drag += 0.05f;
            }
        }
    }

    IEnumerator StartDrop()
    {
        dropCooldown = true;
        transform.localEulerAngles = new Vector3(0, 0, dropAngles[Random.Range(0,dropAngles.Count)]);
        Debug.Log(transform.localEulerAngles);
        rigidBody.AddForce(transform.up * itemDropForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1);
        dropCooldown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerTag")
        {
            Destroy(gameObject);
        }
    }

}
