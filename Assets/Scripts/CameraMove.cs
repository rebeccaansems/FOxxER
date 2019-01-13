using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraSpeed;

    private void Update()
    {
        this.transform.position = this.transform.position + Vector3.forward * Time.deltaTime * cameraSpeed;
    }
}
