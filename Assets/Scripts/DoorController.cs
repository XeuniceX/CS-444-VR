using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator; // Assign in Inspector
    public bool isLocked = true;  // Start locked

    void Start()
    {
        isLocked = true; // Ensure the door starts locked
    }

    // Call this when key is placed
    public void UnlockDoor()
    {
        if (isLocked)
        {
            isLocked = false;
            doorAnimator.SetTrigger("DoorOpen"); // Set trigger in Animator
        }
    }
}