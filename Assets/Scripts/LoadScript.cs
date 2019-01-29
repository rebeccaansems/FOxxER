using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScript : MonoBehaviour
{
    public bool destroyOnLoad;

    // Start is called before the first frame update
    void Start()
    {
        if (destroyOnLoad)
        {
            Destroy(this.gameObject);
        }
    }
}
