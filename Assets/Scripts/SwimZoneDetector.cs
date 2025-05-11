using UnityEngine;

public class SwimZoneDetector : MonoBehaviour
{
    public Swimmer swimmer;
    private Rigidbody swimmerRigidbody;
    public AudioClip waterSound;

    // Define how deep the player must be to activate swim mode (adjust as needed)

    private bool isInSwimZone = false;

    void Start()
    {
        if (swimmer != null)
        {
            swimmerRigidbody = swimmer.GetComponent<Rigidbody>();
            Debug.Log("Gravity:" + swimmerRigidbody.useGravity);
            Debug.Log("SwimZoneDetector started");
        }
    }

    void Update()
    {
        if (isInSwimZone && swimmer != null && swimmerRigidbody != null)
        {

            // Only activate swimming when player is below threshold
            if (!swimmer.IsSwimmingEnabled())
            {
                Debug.Log("Activated Swimming");
                swimmerRigidbody.useGravity = false;
                swimmer.SetSwimmingEnabled(true);
                
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SwimZone"))
        {
            Debug.Log("Entered Swim Zone");
            isInSwimZone = true;
            
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = waterSound;
            if (audioSource != null && waterSound != null)
                audioSource.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SwimZone"))
        {
            Debug.Log("Exited Swim Zone");
            isInSwimZone = false;
            swimmer.SetSwimmingEnabled(false);
            if (swimmerRigidbody != null)
            {
                swimmerRigidbody.useGravity = true;
            }
        }
    }
}