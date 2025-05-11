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

    private bool wasInFOVLastFrame = false; // Keeps track of previous frame
    private bool currentlyScreaming = false; // Track if scream is playing
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
        bool isInFOV = IsControllerInCameraFOV(leftController) || IsControllerInCameraFOV(rightController);

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
                sanityBarController.DecreaseSanity(0.2f);
                Debug.Log("Sanity Decreased from Screaming Monster");
            }
        }


        wasInFOVLastFrame = isInFOV;
    }

    void LateUpdate()
    {
        if (mirrorCamera != null)
        {
            mirrorCamera.Render();
        }
    }

    bool IsControllerInCameraFOV(Transform controller)
    {
        if (controller == null || mirrorCamera == null)
            return false;

        Vector3 viewportPos = mirrorCamera.WorldToViewportPoint(controller.position);

        float marginX = 0.2f;
        float marginY = 0.3f;

        return viewportPos.z > 0 &&
               viewportPos.x >= marginX && viewportPos.x <= (1 - marginX) &&
               viewportPos.y >= marginY && viewportPos.y <= (1 - marginY);
    }

    void StartScreaming()
    {
        Debug.Log("Started Screaming");

        currentlyScreaming = true;

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.loop = true;  // Force loop
            audioSource.Play();
        }

        if (zombieAnimator != null)
        {
            Debug.Log("Setting shouldscream true on zombie animator");
            zombieAnimator.SetBool("ShouldScream", true);
            Debug.Log("Animator Should Scream: " + zombieAnimator.GetBool("ShouldScream"));
            Debug.Log("current animator state: " + zombieAnimator.GetCurrentAnimatorStateInfo(0).IsName("mixamo_com"));
        }
    }

    void StopScreaming()
    {
        Debug.Log("Stopped Screaming");

        currentlyScreaming = false;

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false; // Reset loop state if needed
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
        Debug.Log("S recognized! Dispelling the monster!");

        isDispelled = true;

        currentlyScreaming = false;

        if (audioSource != null)
        {
            audioSource.loop = false;
        }

        if (zombieAnimator != null)
        {
            zombieAnimator.SetBool("ShouldScream", false);
        }
    }
}
