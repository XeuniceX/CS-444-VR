using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText; // Assign in Inspector
    private AudioSource audioSource;
    
    public AudioClip entryClip;  

    public void EndGame()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        Debug.Log("Game Over!");
        if (gameOverText != null)
            gameOverText.SetActive(true);
        audioSource.clip = entryClip;
        audioSource.Play();
    }
}