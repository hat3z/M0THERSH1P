using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSV2_CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed;
    //void LateUpdate()
    //{
    //   transform.position = player.position + offset;
    //}

    void FixedUpdate()
    {
        Vector3 desiredPos = player.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothPos;
    }
}
