using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] float zRotation;
    [SerializeField] float rotationSpeed;
    void Update()
    {
        float sin = Mathf.Sin(Time.time * rotationSpeed);
        transform.eulerAngles = new Vector3 (0, 0, zRotation * sin);
    }
}
