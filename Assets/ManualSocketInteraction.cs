using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class ManualSocketInteraction : XRSocketInteractor
{
    [Header("XR Settings")]
    public GameObject debugController;
    public GameObject leftDebugController;
    public GameObject rightDebugController;
    public float activationThreshold = 0.5f;

    [Header("Crafting Settings")]
    public float lookThreshold = 0.5f;
    private Camera mainCamera;
    public float craftingDuration = 3f;
    public ParticleSystem craftingEffect;
    public GameObject parent;
    public GameObject product;
    public GameObject loadingScreen;
    public Slider slider;
    public string craftingTag;

    private Collider _socketCollider;
    private XRSocketInteractor _socketInteractor;
    private XRInteractionManager _interactionManager;
    private GameObject _potentialInteractableObject;
    private XRGrabInteractable _potentialInteractable;
    private float _craftingProgress;
    private bool _isCrafting;
    
    InputDevice _leftControllerDevice;
    InputDevice _rightControllerDevice;
    Vector3 _leftControllerVelocity;
    Vector3 _rightControllerVelocity;
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

    void Start()
    {
        _socketCollider = GetComponent<Collider>();
        _socketInteractor = GetComponent<XRSocketInteractor>();
        _interactionManager = _socketInteractor.interactionManager;

        _socketInteractor.interactionLayers = InteractionLayerMask.GetMask("None");
        
        InitializeController();
        DebugControllers();
        mainCamera = Camera.main;
        Debug.Log(mainCamera); 
        Debug.Log("Testing");
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered trigger: " + other.name);
        if (other.CompareTag(craftingTag) && other.GetComponent<XRGrabInteractable>() != null)
        {
            Debug.Log("Craftable object detected: " + other.name);
            _potentialInteractableObject = other.gameObject;
            _potentialInteractable = other.GetComponent<XRGrabInteractable>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object exited trigger: " + other.name);
        if (other.CompareTag("Craftable"))
        {
            Debug.Log("Craftable object exited: " + other.name);
            _potentialInteractable = null;
            if (_isCrafting) CancelCrafting();
        }
    }

    void DebugControllers()
    {
        List<UnityEngine.XR.InputDevice> controllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, controllers);

        Debug.Log(string.Format("Found {0} devices with characteristics '{1}'", controllers.Count, desiredCharacteristics.ToString()));

        if (controllers.Count > 0)
        {
            debugController.SetActive(true);
            return;
        }
        foreach (var device in controllers)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics.ToString()));
        }
    }

    void InitializeController()
    {
        _leftControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        _rightControllerDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (!_leftControllerDevice.isValid)
        {
            Debug.LogError($"Controller not found for {XRNode.LeftHand}");
        }

        if (!_rightControllerDevice.isValid)
        {
            Debug.LogError($"Controller not found for {XRNode.RightHand}");
        }
    }

    void Update()
    {
        HandleControllerInput();
        UpdateCraftingProcess();
        // UpdateInput();
    }

    void HandleControllerInput()
    {
        _leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out _leftControllerVelocity);
        _rightControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out _rightControllerVelocity);
        leftDebugController.SetActive(_leftControllerVelocity.magnitude > activationThreshold);
        rightDebugController.SetActive(_rightControllerVelocity.magnitude > activationThreshold);
        
        Debug.Log(mainCamera.transform.position);

        if (mainCamera == null)
        {
            return;
        }
        
        Vector3 toSocket = (transform.position - mainCamera.transform.position).normalized;
        float dot = Vector3.Dot(mainCamera.transform.forward, toSocket);

        // Show mesh if looking at it
        if (dot <= lookThreshold)
        {
            return;
        }

        if ( (_leftControllerVelocity.magnitude > activationThreshold ||
              _rightControllerVelocity.magnitude > activationThreshold) && _potentialInteractable != null)
        {
            if (!_isCrafting) StartCrafting();
        }
        else
        {
            if (_isCrafting) CancelCrafting();
        }
    }
    
    void StartCrafting()
    {
        _isCrafting = true;
        _craftingProgress = 0f;
        loadingScreen.SetActive(true);
        craftingEffect.Play();
        slider.value = 0f;
        Debug.Log("Crafting started");
    }

    void CancelCrafting()
    {
        _isCrafting = false;
        _craftingProgress = 0f;
        UpdateProgressVisuals(0f);
        loadingScreen.SetActive(false);
        craftingEffect.Stop();
        Debug.Log("Crafting canceled");
    }

    void UpdateCraftingProcess()
    {
        if (!_isCrafting) return;

        _craftingProgress += Time.deltaTime / craftingDuration;
        _craftingProgress = Mathf.Clamp01(_craftingProgress);
        slider.value = _craftingProgress;

        if (_craftingProgress >= 1f)
        {
            CompleteCrafting();
        }
    }

    void CompleteCrafting()
    {
        // _interactionManager.ForceSelect(_socketInteractor, _potentialInteractable);
        _isCrafting = false;
        _craftingProgress = 0f;
        UpdateProgressVisuals(0f);
        _potentialInteractableObject.SetActive(false);
        _potentialInteractable = null;
        loadingScreen.SetActive(false);
        product.SetActive(true);
        product.transform.parent = null;
        craftingEffect.Stop();
        Debug.Log("Crafting completed");
        
        parent.SetActive(false);
    }

    void UpdateProgressVisuals(float progress)
    {
        if (slider != null)
        {
            slider.value = _craftingProgress;
        }
    }
}
