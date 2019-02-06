using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class OverallController : MonoBehaviour
{
    public bool isMuted = false;
    public int currentLevel = 0;
    public int musicVolume = 0;
    public int sfxVolume = 1;
    public int gamesPlayed = 0;
    
    public int[] unlockScores;

    public string[] levelName;

    public static OverallController instance = null;

#if UNITY_IOS
    private const string gameID = "3032848";
#elif UNITY_ANDROID
    private const string gameID = "3032849";
#elif UNITY_EDITOR
    private const string gameID = "1111111";
#endif

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

    void Start()
    {
        Advertisement.Initialize(gameID);
    }

    private void Update()
    {
        musicVolume = isMuted ? 0 : 1;
    }
}
