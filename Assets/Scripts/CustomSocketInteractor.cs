using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class CustomSocketInteractor : XRSocketInteractor
{
    [Header("Socket Settings")]
    public string targetTag;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        Debug.Log(base.CanHover(interactable) && interactable.transform.CompareTag(targetTag));
        Debug.Log(targetTag);
        return base.CanHover(interactable) && interactable.transform.CompareTag(targetTag);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return false;
    }
}