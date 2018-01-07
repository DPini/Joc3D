using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource[] soundtrack;
    public AudioSource actualSoundtrack;

    public AudioSource menuMusic;

    public AudioClip jumpEffect;
    public AudioClip splashEffect;

    public AudioClip carCrash;



    public AudioSource soundEffects;

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
       CancelInvoke();
       if (actualSoundtrack != null) actualSoundtrack.Stop();
       menuMusic.Play();
       Invoke("EnterMenu", menuMusic.clip.length);
    }

    public void ExitMenu()
    {
        menuMusic.Stop();
        PlaySoundtrack();
    }

    public void PlaySoundtrack() {

        if (actualSoundtrack != null) actualSoundtrack.Stop();

        soundtrack[i].Play();
        actualSoundtrack = soundtrack[i];

        if (i == 0)
        {
            i = 1;
        }
        else i = 0;
        Invoke("PlaySoundtrack", actualSoundtrack.clip.length);
    }

    public void Jump() {
        soundEffects.PlayOneShot(jumpEffect);
    }

    public void Splash() {
        soundEffects.PlayOneShot(splashEffect);
    }

    public void CarCrash() {
        soundEffects.PlayOneShot(carCrash);

    }

    /**

    private void AudioFadeOut(AudioSource audioSource) {

        float startVolume = audioSource.volume;
        Debug.Log("StartVolume: " + startVolume);

        while (audioSource.volume > 0)
        {
            Debug.Log("new volume: " + audioSource.volume);
            audioSource.volume -= startVolume * Time.deltaTime / 20000.0f;

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
            audioSource.volume += startVolume * Time.deltaTime / 5.0f;

        }
    }
    */

    void Update()
    {
        
    }

}