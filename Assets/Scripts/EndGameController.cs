using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public void RestartGame()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return null; // wait 1 frame
        Debug.Log("⏳ Now loading Project Scene...");
        SceneManager.LoadScene("Project Scene", LoadSceneMode.Single);
    }
}
