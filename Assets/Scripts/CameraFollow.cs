using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 cameraPos;
    public bool cameraFollowOn;
    
    private Vector3 playerPos;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (cameraFollowOn)
        {
            playerPos = new Vector3(player.position.x + cameraPos.x, cameraPos.y, player.position.z + cameraPos.z);
            transform.position = Vector3.Lerp(transform.position, playerPos, 0.1f);
        }
    }
}
