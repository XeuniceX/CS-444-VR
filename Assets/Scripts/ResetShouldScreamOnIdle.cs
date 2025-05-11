using UnityEngine;

public class ResetShouldScreamOnIdle : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("ShouldScream", false);
        // Debug.Log("Resetting ShouldScream to false on Idle entry");

        // Important: Force re-evaluation cleanly by resetting the Animator tick
        animator.Rebind();     // Rebind resets the animator to base evaluation state
        animator.Update(0f);   // Forces an update frame to evaluate Idle state properly
    }
}
