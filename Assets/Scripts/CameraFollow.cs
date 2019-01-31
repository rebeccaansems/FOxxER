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
        player = GameController.instance.player.transform;
    }

    void FixedUpdate()
    {
        if (cameraFollowOn)
        {
            playerPos = new Vector3(player.position.x + cameraPos.x, cameraPos.y, player.position.z + cameraPos.z);
            transform.position = Vector3.Lerp(transform.position, playerPos, 0.05f);
        }
    }
}
