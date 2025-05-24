using UnityEngine;
using TMPro;
using System.Collections;

public class ToiletDoorVoice : MonoBehaviour
{
    public AudioClip entryClip;
    public AudioClip exitClip;

    public TMP_Text entrySubtitle;
    public TMP_Text exitSubtitle;
    public float fadeDuration = 0.5f;

    private AudioSource audioSource;
    private bool hasPlayedEntry = false;
    private bool hasPlayedExit = false;
    private bool playerIsInside = false;
    private bool isPlayingRecording = false;
    private Coroutine currentSequence;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        SetAlpha(entrySubtitle, 0f);
        SetAlpha(exitSubtitle, 0f);
    }

    private void Update()
    {
        if (isPlayingRecording && !audioSource.isPlaying)
        {
            isPlayingRecording = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;

            if (!hasPlayedEntry && !isPlayingRecording)
            {
                hasPlayedEntry = true;
                isPlayingRecording = true;
                if (currentSequence != null) StopCoroutine(currentSequence);
                currentSequence = StartCoroutine(PlayClipWithSubtitle(entryClip, entrySubtitle));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerIsInside && !hasPlayedExit && !isPlayingRecording)
            {
                hasPlayedExit = true;
                isPlayingRecording = true;
                if (currentSequence != null) StopCoroutine(currentSequence);
                currentSequence = StartCoroutine(PlayClipWithSubtitle(exitClip, exitSubtitle));
            }

            playerIsInside = false;
        }
    }

    private IEnumerator PlayClipWithSubtitle(AudioClip clip, TMP_Text subtitle)
    {
        if (clip != null && subtitle != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            yield return FadeIn(subtitle);
            yield return new WaitForSeconds(clip.length);
            yield return FadeOut(subtitle);
        }
        isPlayingRecording = false;
    }

    private IEnumerator FadeIn(TMP_Text text)
    {
        yield return FadeText(text, 1f, fadeDuration);
    }

    private IEnumerator FadeOut(TMP_Text text)
    {
        yield return FadeText(text, 0f, fadeDuration);
    }

    private IEnumerator FadeText(TMP_Text text, float targetAlpha, float duration)
    {
        float startAlpha = text.color.a;
        float timer = 0f;
        Color c = text.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            text.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
        text.color = new Color(c.r, c.g, c.b, targetAlpha);
    }

    private void SetAlpha(TMP_Text text, float alpha)
    {
        if (text != null)
        {
            Color c = text.color;
            text.color = new Color(c.r, c.g, c.b, alpha);
        }
    }

    public void ResetTrigger()
    {
        hasPlayedEntry = false;
        hasPlayedExit = false;
        playerIsInside = false;
        isPlayingRecording = false;
        audioSource.Stop();

        SetAlpha(entrySubtitle, 0f);
        SetAlpha(exitSubtitle, 0f);

        if (currentSequence != null)
        {
            StopCoroutine(currentSequence);
            currentSequence = null;
        }

        Debug.Log("Trigger has been reset");
    }
}
