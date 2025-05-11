using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText; // Assign in Inspector

    public void EndGame()
    {
        Debug.Log("Game Over!");
        if (gameOverText != null)
            gameOverText.SetActive(true);
    }
}