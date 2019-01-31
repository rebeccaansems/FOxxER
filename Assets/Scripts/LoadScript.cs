using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScript : MonoBehaviour
{
    public bool destroyOnLoad;

    void Start()
    {
        if (destroyOnLoad)
        {
            Destroy(this.gameObject);
        }
    }
}
