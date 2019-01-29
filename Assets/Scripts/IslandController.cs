using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandController : MonoBehaviour
{
    private int chunkNumber;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetupIsland(int chunk)
    {
        chunkNumber = chunk;
    }

    private void Update()
    {
        if (player.transform.position.z > 42 * (chunkNumber + 1.5))
        {
            Destroy(this.gameObject);
        }
    }
}
