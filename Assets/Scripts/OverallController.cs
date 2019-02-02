using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallController : MonoBehaviour
{
    public bool isMuted = false;
    public int currentLevel = 0;

    public static OverallController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
