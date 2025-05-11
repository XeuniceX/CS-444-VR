using UnityEngine;

public class ResetAnimatorOnStart : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            // Fully reset Animator
            animator.enabled = false;     // Turn off Animator
            animator.Rebind();             // Reset all bindings
            animator.Update(0f);            // Force one frame update
            animator.enabled = true;       // Turn Animator back on
        }
    }
}
