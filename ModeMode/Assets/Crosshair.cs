using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] Canvas canvasRef;

    private void Start()
    {
        Cursor.visible = false;

    }

    void LateUpdate()
    {
        transform.position = (Input.mousePosition);
    }
}
