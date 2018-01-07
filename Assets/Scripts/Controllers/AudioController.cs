using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource[] soundtrack;
    public AudioSource actualSoundtrack;

    public int i = 0;

    public float bpm = 128;

    private float m_TransitionIn;
    private float m_TransitionOut;
    private float m_QuarterNote;

    // Use this for initialization
    void Start()
    {
        PlaySoundtrack();
    }

    public void EnterMenu()
    {
       
    }

    public void PlaySoundtrack() {
        if (actualSoundtrack != null) AudioFadeOut(actualSoundtrack);
        
        AudioFadeIn(soundtrack[i]);
        actualSoundtrack = soundtrack[i];

        if (i == 0)
        {
            i = 1;
        }
        else i = 0;
        Invoke("PlaySoundtrack", actualSoundtrack.clip.length - 1.0f);
    }

    private void AudioFadeOut(AudioSource audioSource) {

        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1.0f;

        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    

    private void AudioFadeIn(AudioSource audioSource)
    {

        float startVolume = audioSource.volume;
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / 1.0f;

        }
    }

}