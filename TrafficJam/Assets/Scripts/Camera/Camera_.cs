using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ : MonoBehaviour
{
    public bool toPoints;
    public Transform CameraObj;
    public Transform cameraPoint, arrowLook, cameraLook;
    public float spead;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (toPoints) {
            transform.position = Vector3.Lerp(transform.position, cameraPoint.position, Time.deltaTime * spead);
            arrowLook.LookAt(cameraLook);
            CameraObj.rotation = Quaternion.Lerp(CameraObj.rotation, arrowLook.rotation, Time.deltaTime * 2);
        }
    }
}