using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// control the camera as you please
public class CameraController : MonoBehaviour
{
    public float distanceBetweenObjectsX = 0f;
    public float distanceBetweenObjectsY = 10f;
    public float distanceBetweenObjectsZ = -4f;

    public float angleX = 64f;
    public float angleY = 0f;
    public float angleZ = 0f;

    private void Awake()
    {
        transform.localPosition = new Vector3(0, distanceBetweenObjectsY, 0);
        
        transform.localEulerAngles = new Vector3(angleX, angleY, angleZ);
    }
}
