using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraPos;

    private Vector3 targetPos;

    void Update()
    {
        targetPos = new Vector3(target.position.x + cameraPos.x, cameraPos.y, target.position.z + cameraPos.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
    }
}
