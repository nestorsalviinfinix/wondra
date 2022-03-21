using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera cameraFollow;

    private void Start()
    {
        if (cameraFollow == null)
            cameraFollow = Camera.main;
    }
    public void SetCamera(Camera c)
    {
        cameraFollow = c;
    }

    private void LateUpdate()
    {
        transform.forward = cameraFollow.transform.forward;
    }
}
