using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawning : MonoBehaviour
{
    public GameObject[] islandChunks;
    public Transform player;

    private int numberSpawnedChunks = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        while (numberSpawnedChunks < 3)
        {
            SpawnChunk();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > 42 * (numberSpawnedChunks - 3))
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        GameObject newIslandChunk = Instantiate(islandChunks[Random.Range(0, islandChunks.Length)], this.gameObject.transform);
        newIslandChunk.transform.position = new Vector3(0, 0, numberSpawnedChunks * 42);
        newIslandChunk.GetComponent<IslandController>().SetupIsland(numberSpawnedChunks);
        numberSpawnedChunks++;
    }
}
