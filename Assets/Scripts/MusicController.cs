using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Image soundOn, soundOff;

    // Start is called before the first frame update
    void Start()
    {
        soundOn.enabled = !OverallController.instance.isMuted;
        soundOff.enabled = OverallController.instance.isMuted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
