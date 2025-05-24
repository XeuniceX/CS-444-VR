using UnityEngine;
using TMPro;
using System.Collections;

public class PoolVoiceTrigger : MonoBehaviour
{
    public AudioClip entryClip;
    public AudioClip secondClip;
    public AudioClip thirdClip;
    public AudioClip fourthClip;

    public TMP_Text entrySubtitle;
    public TMP_Text secondSubtitle;
    public TMP_Text thirdSubtitle;
    public TMP_Text fourthSubtitle;

    public float fadeDuration = 0.5f;

    private AudioSource audioSource;
    private bool hasPlayedEntry = false;
    private Coroutine fadeCoroutine;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnEnable()
    {
        // Ensure subtitle GameObjects are active and invisible
        ActivateAndResetSubtitle(entrySubtitle);
        ActivateAndResetSubtitle(secondSubtitle);
        ActivateAndResetSubtitle(thirdSubtitle);
        ActivateAndResetSubtitle(fourthSubtitle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayedEntry && other.CompareTag("Player"))
        {
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
            yield return FadeIn(entrySubtitle);
            yield return new WaitForSeconds(entryClip.length);
            yield return FadeOut(entrySubtitle);
        }

        if (secondClip != null)
        {
            audioSource.clip = secondClip;
            audioSource.Play();
            yield return FadeIn(secondSubtitle);
            yield return new WaitForSeconds(secondClip.length);
            yield return FadeOut(secondSubtitle);
        }

        if (thirdClip != null)
        {
            audioSource.clip = thirdClip;
            audioSource.Play();
            yield return FadeIn(thirdSubtitle);
            yield return new WaitForSeconds(thirdClip.length);
            yield return FadeOut(thirdSubtitle);
        }

        if (fourthClip != null)
        {
            audioSource.clip = fourthClip;
            audioSource.Play();
            yield return FadeIn(fourthSubtitle);
            yield return new WaitForSeconds(fourthClip.length);
            yield return FadeOut(fourthSubtitle);
        }
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

    private void ActivateAndResetSubtitle(TMP_Text text)
    {
        if (text != null)
        {
            text.gameObject.SetActive(true);
            SetAlpha(text, 0f);
        }
    }
}
