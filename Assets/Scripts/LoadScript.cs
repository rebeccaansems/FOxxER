using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScript : MonoBehaviour
{
    public bool destroyOnLoad;
    public bool dontDestroyOnLoad;

    // Start is called before the first frame update
    void Start()
    {
        if (destroyOnLoad)
        {
            Destroy(this.gameObject);
        }

        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
            if (GameObject.FindGameObjectsWithTag(this.gameObject.tag).Length > 1)
            {
                Destroy(GameObject.FindGameObjectsWithTag(this.gameObject.tag)[0]);
            }
        }
    }
}
