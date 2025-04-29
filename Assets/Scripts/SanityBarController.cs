using UnityEngine;
using UnityEngine.UI;

public class SanityBarController : MonoBehaviour
{
    public Image sanityBarImage; // Assign your Image here
    public float decreaseAmount = 0.05f; // Amount to decrease per interval (0 to 1)
    public float decreaseInterval = 1f;  // Seconds between decreases

    private float timer = 0f;

    void Start()
    {
        if (sanityBarImage == null)
            Debug.LogError("Sanity Bar Image not assigned!");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= decreaseInterval)
        {
            timer = 0f;
            DecreaseSanity();
        }
    }

    void DecreaseSanity()
    {
        if (sanityBarImage.fillAmount > 0)
        {
            sanityBarImage.fillAmount -= decreaseAmount;
            Debug.Log("Sanity: " + sanityBarImage.fillAmount); // Add this line
            if (sanityBarImage.fillAmount <= 0)
            {
                Debug.Log("Sanity depleted!");
                sanityBarImage.fillAmount = 0;
                
            }
        }
    }
}