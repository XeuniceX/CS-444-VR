using UnityEngine;

public class ToiletDoorVoice : MonoBehaviour
{
    public AudioClip entryClip;
    public AudioClip exitClip;

    private AudioSource audioSource;
    private bool hasPlayedEntry = false;
    private bool hasPlayedExit = false;
    private bool playerIsInside = false;
    private bool isPlayingRecording = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        Debug.Log("DoorVoiceTrigger initialized");
    }

    private void Update()
    {
        // Check if a recording was playing but has now finished
        if (isPlayingRecording && !audioSource.isPlaying)
        {
            isPlayingRecording = false;
            Debug.Log("Recording finished playing");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            
            // Mark player as inside FIRST
            playerIsInside = true;
            
            // Only play entry clip if it hasn't been played before and no recording is playing
            if (!hasPlayedEntry && !isPlayingRecording)
            {
                Debug.Log("Playing entry clip");
                audioSource.Stop();
                audioSource.clip = entryClip;
                audioSource.Play();
                hasPlayedEntry = true;
                isPlayingRecording = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger, playerIsInside=" + playerIsInside + ", isPlayingRecording=" + isPlayingRecording);
            
            // Only play exit clip if:
            // 1. Player was inside
            // 2. Exit clip hasn't been played before
            // 3. No recording is currently playing
            if (playerIsInside && !hasPlayedExit && !isPlayingRecording)
            {
                Debug.Log("Playing exit clip");
                audioSource.Stop();
                audioSource.clip = exitClip;
                audioSource.Play();
                hasPlayedExit = true;
                isPlayingRecording = true;
            }
            
            // Mark player as outside AFTER deciding to play the clip
            playerIsInside = false;
        }
    }

    // Add this method to your component and call it from the inspector for testing
    public void ResetTrigger()
    {
        hasPlayedEntry = false;
        hasPlayedExit = false;
        playerIsInside = false;
        isPlayingRecording = false;
        audioSource.Stop();
        Debug.Log("Trigger has been reset");
    }
}