using UnityEngine;
using TMPro; // Make sure you have TextMeshPro package installed
using System.Collections;

public class SimpleVoicePlayback : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip backgroundClip;

    [Header("Subtitles")]
    public TMP_Text subtitle1;
    public TMP_Text subtitle2;
    public TMP_Text subtitle3;
    public TMP_Text subtitle4;
    public TMP_Text subtitle5;
    public float fadeDuration = 0.5f;

    private AudioSource backgroundSource;
    private AudioSource voiceSource;

    void Start()
    {
        // Create a dedicated background AudioSource
        backgroundSource = gameObject.AddComponent<AudioSource>();
        backgroundSource.clip = backgroundClip;
        backgroundSource.loop = true;
        backgroundSource.playOnAwake = false;
        backgroundSource.volume = 0.5f;
        backgroundSource.Play();

        // Create a separate AudioSource for the voice clips
        voiceSource = gameObject.AddComponent<AudioSource>();
        voiceSource.playOnAwake = false;

        // Hide all subtitles at start
        SetAlpha(subtitle1, 0f);
        SetAlpha(subtitle2, 0f);
        SetAlpha(subtitle3, 0f);
        SetAlpha(subtitle4, 0f);
        SetAlpha(subtitle5, 0f);

        StartCoroutine(PlayClips());
    }

    IEnumerator PlayClips()
    {
        // Clip 1
        voiceSource.clip = clip1;
        voiceSource.Play();
        yield return FadeIn(subtitle1);
        yield return new WaitForSeconds(clip1.length);
        yield return FadeOut(subtitle1);

        // Clip 2
        voiceSource.clip = clip2;
        voiceSource.Play();
        yield return FadeIn(subtitle2);
        yield return new WaitForSeconds(clip2.length);
        yield return FadeOut(subtitle2);

        // Clip 3
        voiceSource.clip = clip3;
        voiceSource.Play();
        yield return FadeIn(subtitle3);
        yield return new WaitForSeconds(clip3.length);
        yield return FadeOut(subtitle3);
        
        voiceSource.clip = clip4;
        voiceSource.Play();
        yield return FadeIn(subtitle4);
        yield return new WaitForSeconds(clip4.length);
        yield return FadeOut(subtitle4);
        
        voiceSource.clip = clip5;
        voiceSource.Play();
        yield return FadeIn(subtitle5);
        yield return new WaitForSeconds(clip5.length);
        yield return FadeOut(subtitle5);
    }

    IEnumerator FadeIn(TMP_Text text)
    {
        yield return FadeText(text, 1f, fadeDuration);
    }

    IEnumerator FadeOut(TMP_Text text)
    {
        yield return FadeText(text, 0f, fadeDuration);
    }

    IEnumerator FadeText(TMP_Text text, float targetAlpha, float duration)
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

    void SetAlpha(TMP_Text text, float alpha)
    {
        if (text != null)
        {
            Color c = text.color;
            text.color = new Color(c.r, c.g, c.b, alpha);
        }
    }
}
