using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour
{
    public AudioClip[] AudioClips;

    public void Play(int num)
    {
        this.GetComponent<AudioSource>().clip = AudioClips[num];
        this.GetComponent<AudioSource>().volume = OverallController.instance.sfxVolume;
        this.GetComponent<AudioSource>().Play();
    }

    public void Play()
    {
        Play(0);
    }

    public void PlayRandom()
    {
        Play(Random.Range(0, AudioClips.Length));
    }

    public void PlayRandom(int min, int max)
    {
        Play(Random.Range(min, max));
    }

}