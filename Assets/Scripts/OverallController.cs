using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallController : MonoBehaviour
{
    public bool isMuted = false;
    public int currentLevel = 0;
    public int musicVolume = 0;
    public int sfxVolume = 1;
    
    public int[] unlockScores;

    public static OverallController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        musicVolume = isMuted ? 0 : 1;
    }
}
