using UnityEngine;
using System.Collections;

public class DoorVoiceTrigger : MonoBehaviour
{
    public AudioClip entryClip;       // First clip to play
    public AudioClip secondClip;      // Second clip to play after entryClip
    public AudioClip thirdClip;

    private AudioSource audioSource;
    private bool hasPlayedEntry = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayedEntry && other.CompareTag("Player"))
        {
            Debug.Log("Playing entry and second clips in sequence");
            StartCoroutine(PlayEntrySequence());
            hasPlayedEntry = true;
        }
    }

    private IEnumerator PlayEntrySequence()
    {
        if (entryClip != null)
        {
            audioSource.clip = entryClip;
            audioSource.Play();
            yield return new WaitForSeconds(entryClip.length);
        }

        if (secondClip != null)
        {
            audioSource.clip = secondClip;
            audioSource.Play();
            yield return new WaitForSeconds(secondClip.length);
        }
        
        if (thirdClip != null)
        {
            audioSource.clip = thirdClip;
            audioSource.Play();
        }
    }
}