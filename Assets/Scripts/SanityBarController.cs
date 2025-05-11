using UnityEngine;
using UnityEngine.UI;

public class SanityBarController : MonoBehaviour
{
    public Image sanityBarImage; // Assign your Image here
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
            DecreaseSanity(0.005f);
        }
    }

    public void DecreaseSanity(float decreaseAmount)
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
    
    public void IncreaseSanity(float increaseAmount)
    {
        sanityBarImage.fillAmount += increaseAmount;
        if (sanityBarImage.fillAmount > 1f)
            sanityBarImage.fillAmount = 1f;
        Debug.Log("Sanity Increase ");
        Debug.Log("Sanity: " + sanityBarImage.fillAmount);
    }

}