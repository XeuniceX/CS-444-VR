using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SanityBarController : MonoBehaviour
{
    public Image sanityBarImage; // Assign your Image here
    public float decreaseInterval = 1f;  // Seconds between decreases
    public GameObject sanityDepletedUI;
    private AudioSource audioSource;

    public AudioClip entryClip;
    private float timer = 0f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        if (sanityBarImage == null)
            Debug.LogError("Sanity Bar Image not assigned!");

        // Hide the UI initially
        if (sanityDepletedUI != null)
            sanityDepletedUI.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= decreaseInterval)
        {
            timer = 0f;
            DecreaseSanity(0.005f);
        }
    }

    public void DecreaseSanity(float decreaseAmount)
    {
        if (sanityBarImage.fillAmount > 0)
        {
            sanityBarImage.fillAmount -= decreaseAmount;
            Debug.Log("Sanity: " + sanityBarImage.fillAmount);

            if (sanityBarImage.fillAmount <= 0)
            {
                sanityBarImage.fillAmount = 0;
                Debug.Log("Sanity depleted!");

                if (entryClip != null)
                {
                    audioSource.clip = entryClip;
                    audioSource.Play();
                    StartCoroutine(ShowDepletedUIAfterDelay(5f));
                }
                else
                {
                    Debug.LogWarning("entryClip is not assigned!");
                }
            }
        }
    }

    public void IncreaseSanity(float increaseAmount)
    {
        sanityBarImage.fillAmount += increaseAmount;
        if (sanityBarImage.fillAmount > 1f)
            sanityBarImage.fillAmount = 1f;
        Debug.Log("Sanity Increase ");
        Debug.Log("Sanity: " + sanityBarImage.fillAmount);
    }

    private IEnumerator ShowDepletedUIAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        if (sanityDepletedUI != null)
            sanityDepletedUI.SetActive(true);
    }
}
