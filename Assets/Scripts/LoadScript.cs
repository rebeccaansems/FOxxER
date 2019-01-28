using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScript : MonoBehaviour
{
    public bool destroyLoad;

    // Start is called before the first frame update
    void Start()
    {
        if (destroyLoad)
        {
            Destroy(this.gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
