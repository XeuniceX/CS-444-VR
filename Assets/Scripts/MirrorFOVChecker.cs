using UnityEngine;

public class MirrorFOVChecker : MonoBehaviour
{
    [Header("Camera to Check FOV")]
    public Camera mirrorCamera;

    [Header("XR Controllers")]
    public Transform leftController;
    public Transform rightController;

    [Header("SFX and Animation")]
    public AudioSource audioSource;
    public Animator zombieAnimator;
    public SanityBarController sanityBarController;
    
    private bool currentlyScreaming = false;
    private bool isDispelled = false;
    
    private float screamSanityTimer = 0f;
    private float screamSanityInterval = 1f;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on " + gameObject.name + ". Please add one and assign an AudioClip.");
        }

        if (zombieAnimator == null)
        {
            Debug.LogWarning("No Animator assigned for the Zombie. Please assign it in the inspector.");
        }
    }

    void Update()
    {
        bool isInFOV = IsInMirrorFOV(leftController) || IsInMirrorFOV(rightController);

        if (isInFOV && !currentlyScreaming && !isDispelled)
        {
            StartScreaming();
        }
        else if (!isInFOV && currentlyScreaming)
        {
            StopScreaming();
        }

        if (currentlyScreaming && !isDispelled && sanityBarController != null)
        {
            screamSanityTimer += Time.deltaTime;
            if (screamSanityTimer >= screamSanityInterval)
            {
                screamSanityTimer = 0f;
                sanityBarController.DecreaseSanity(0.1f);
                Debug.Log("Sanity Decreased from Screaming Monster");
            }
        }
    }

    bool IsInMirrorFOV(Transform target)
    {
        if (target == null || mirrorCamera == null)
            return false;

        // Use Unity's built-in frustum check
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mirrorCamera);
        Bounds targetBounds = new Bounds(target.position, Vector3.one * 0.1f); // Small bound around controller

        // Make sure it's visible in the camera frustum
        if (!GeometryUtility.TestPlanesAABB(planes, targetBounds))
            return false;

        // Additionally make sure it's in front of the camera
        Vector3 dirToTarget = (target.position - mirrorCamera.transform.position).normalized;
        float dot = Vector3.Dot(mirrorCamera.transform.forward, dirToTarget);
        if (dot < 0.7f) return false; // Narrow FOV tolerance

        return true;
    }

    bool IsMirrorVisibleToPlayer()
    {
        Camera playerCamera = Camera.main; // or XR rig's camera

        Plane[] playerFrustum = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        Bounds mirrorBounds = new Bounds(mirrorCamera.transform.position, Vector3.one * 1.0f); // Adjust size if needed

        return GeometryUtility.TestPlanesAABB(playerFrustum, mirrorBounds);
    }

    void StartScreaming()
    {
        Debug.Log("Started Screaming");
        currentlyScreaming = true;

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
        }

        if (zombieAnimator != null)
        {
            zombieAnimator.SetBool("ShouldScream", true);
        }
    }

    void StopScreaming()
    {
        Debug.Log("Stopped Screaming");
        currentlyScreaming = false;

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }

        if (zombieAnimator != null)
        {
            zombieAnimator.SetBool("ShouldScream", false);
        }
    }

    void OnEnable()
    {
        AirSketchWithS.OnSRecognized += DispelMonster;
    }

    void OnDisable()
    {
        AirSketchWithS.OnSRecognized -= DispelMonster;
    }

    void DispelMonster()
    {
        if (isDispelled || !currentlyScreaming) return;

        // Only dispel if visible in mirror
        bool isInFOV = IsInMirrorFOV(leftController) || IsInMirrorFOV(rightController);
        if (!isInFOV && !IsMirrorVisibleToPlayer()) return;

        Debug.Log($"{gameObject.name} has been dispelled by S");

        isDispelled = true;
        currentlyScreaming = false;

        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }

        if (zombieAnimator != null)
        {
            zombieAnimator.SetBool("ShouldScream", false);
            zombieAnimator.SetTrigger("Dispel"); // Optional
        }
    }
}