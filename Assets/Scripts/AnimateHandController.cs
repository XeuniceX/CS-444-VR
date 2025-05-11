using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimateHandController : MonoBehaviour
{
    public InputActionReference gripInputActionReferenceAction;
    public InputActionReference triggerInputActionReferenceAction;

    private Animator _handAnimator;
    private float _gripValue;
    private float _triggerValue;
    
    private void Start()
    {
        _handAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        AnimateGrip();
        AnimateTrigger();
    }

    private void AnimateGrip()
    {
        _gripValue = gripInputActionReferenceAction.action.ReadValue<float>();
        _handAnimator.SetFloat("Grip", _gripValue);
    }
    
    private void AnimateTrigger()
    {
        _triggerValue = triggerInputActionReferenceAction.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", _triggerValue);
    }
}
