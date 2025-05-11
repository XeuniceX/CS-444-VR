using UnityEngine;

public class HideOnIdle : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var renderer = animator.GetComponentInChildren<SkinnedMeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;  // Hide the zombie when entering Idle
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var renderer = animator.GetComponentInChildren<SkinnedMeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = true;   // Show the zombie when leaving Idle
        }
    }
}
