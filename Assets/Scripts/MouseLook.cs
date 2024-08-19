using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Camera camera;
    
    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        float rotationX = Input.GetAxis("Mouse X") * 1f;
        float rotationY = Input.GetAxis("Mouse Y") * 0.2f;

        transform.Rotate(Vector3.up, rotationX, Space.World);
        transform.Rotate(Vector3.right, rotationY, Space.World);
    }
}
