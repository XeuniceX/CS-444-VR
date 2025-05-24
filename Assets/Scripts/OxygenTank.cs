using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTank : MonoBehaviour
{

    public GameObject poolWall; // Assign the wall/plane blocking access to the pool
    public GameObject audioTrigger;
    public AudioClip sound;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Oxygen"))
        {
            Debug.Log("Oxygen found");
            
            if (sound != null)
                AudioSource.PlayClipAtPoint(sound, transform.position);
            // Destroy the oxygen tank object
            Destroy(other.gameObject);

            // Remove the wall to allow player to proceed
            if (poolWall != null)
            {
                poolWall.SetActive(false); // You can also use Destroy(poolWall) if preferred
                Debug.Log("Pool wall removed!");
            }
            else
            {
                Debug.LogWarning("Pool wall reference not set!");
            }
            
            if (audioTrigger != null)
            {
                audioTrigger.SetActive(true);
                Debug.Log("Audio trigger enabled!");
            }
            else
            {
                Debug.LogWarning("Audio trigger reference not set!");
            }
        }
    }
}
