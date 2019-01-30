using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{
    public NavMeshSurface[] surfaces;

    private void Start()
    {
        foreach(NavMeshSurface currSurface in surfaces)
        {
            currSurface.BuildNavMesh();
        }
    }
}
