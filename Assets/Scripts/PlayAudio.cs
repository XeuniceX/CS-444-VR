using UnityEngine;
using System.Collections;

public class SimpleVoicePlayback : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip backgroundClip;

    private AudioSource backgroundSource;
    private AudioSource voiceSource;

    void Start()
    {
        // Create a dedicated background AudioSource
        backgroundSource = gameObject.AddComponent<AudioSource>();
        backgroundSource.clip = backgroundClip;
        backgroundSource.loop = true;
        backgroundSource.playOnAwake = false;
        backgroundSource.volume = 0.5f; // Optional: lower volume
        backgroundSource.Play();

        // Create a separate AudioSource for the voice clips
        voiceSource = gameObject.AddComponent<AudioSource>();
        voiceSource.playOnAwake = false;

        StartCoroutine(PlayClips());
    }

    IEnumerator PlayClips()
    {
        voiceSource.clip = clip1;
        voiceSource.Play();
        yield return new WaitForSeconds(clip1.length);

        voiceSource.clip = clip2;
        voiceSource.Play();
        yield return new WaitForSeconds(clip2.length);

        voiceSource.clip = clip3;
        voiceSource.Play();
    }
}