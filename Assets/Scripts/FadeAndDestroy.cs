using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float fadeDuration = 3f;
    private float timer = 0f;
    private Color startColor;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            startColor = lineRenderer.startColor;
        }
    }

    void Update()
    {
        if (lineRenderer == null) return;

        timer += Time.deltaTime;
        float t = timer / fadeDuration;

        Color fadedColor = startColor;
        fadedColor.a = Mathf.Lerp(1f, 0f, t);

        lineRenderer.startColor = fadedColor;
        lineRenderer.endColor = fadedColor;

        if (timer >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}
