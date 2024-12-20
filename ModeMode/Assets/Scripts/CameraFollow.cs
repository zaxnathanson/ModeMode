using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;
    Vector3 offset;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        offset = transform.position;
    }

    // CallUpdate is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
