using UnityEngine;

public class MedicineEater : MonoBehaviour
{
    public AudioClip eatSound;
    public SanityBarController sanityBarController; // Reference to your sanity bar
    public float sanityIncreaseAmount = 1.0f; // Amount to restore

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Medicine"))
        {
            // Play sound at the mouth position
            if (eatSound != null)
                AudioSource.PlayClipAtPoint(eatSound, transform.position);
            Debug.Log("Medicine found");
            // Increase sanity
            if (sanityBarController != null)
                sanityBarController.IncreaseSanity(sanityIncreaseAmount);

            // Destroy the medicine object
            Destroy(other.gameObject);
        }
    }
}