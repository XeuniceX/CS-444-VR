using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class CraftingSystem : MonoBehaviour
{
    // [Header("XR Settings")]
    // public GameObject debugController;
    // public GameObject leftDebugController;
    // public GameObject rightDebugController;

    [Header("Crafting Settings")]
    public float lookThreshold = 0.5f;
    public float activationThreshold = 0.5f;
    public float craftingDuration = 3f;
    public ParticleSystem craftingEffect;
    public GameObject parent;
    public GameObject product;
    public GameObject loadingScreen;
    public Slider slider;
    public string craftingTag;

    private Camera mainCamera;
    private GameObject _potentialInteractableObject;
    private float _craftingProgress;
    private bool _isCrafting;

    InputDevice _leftControllerDevice;
    InputDevice _rightControllerDevice;
    Vector3 _leftControllerVelocity;
    Vector3 _rightControllerVelocity;

    void Start()
    {
        InitializeController();
        //DebugControllers();
        mainCamera = Camera.main;
        Debug.Log(mainCamera);
        Debug.Log("Crafting System Initialized");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered crafting trigger: " + other.name);
        if (other.CompareTag(craftingTag) && other.GetComponent<XRGrabInteractable>() != null)
        {
            Debug.Log("Craftable object detected: " + other.name);
            _potentialInteractableObject = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object exited crafting trigger: " + other.name);
        if (other.CompareTag(craftingTag))
        {
            Debug.Log("Craftable object exited: " + other.name);
            _potentialInteractableObject = null;
            if (_isCrafting) CancelCrafting();
        }
    }

    // void DebugControllers()
    // {
    //     List<InputDevice> controllers = new List<InputDevice>();
    //     var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
    //     InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, controllers);
    //
    //     Debug.Log(string.Format("Found {0} devices with characteristics '{1}'", controllers.Count, desiredCharacteristics.ToString()));
    //
    //     if (controllers.Count <= 0)
    //     {
    //         return;
    //     }
    //
    //     debugController.SetActive(true);
    //     foreach (var device in controllers)
    //     {
    //         Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics.ToString()));
    //     }
    // }

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
    }

    void HandleControllerInput()
    {
        _leftControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out _leftControllerVelocity);
        _rightControllerDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out _rightControllerVelocity);
        //leftDebugController.SetActive(_leftControllerVelocity.magnitude > activationThreshold);
        //rightDebugController.SetActive(_rightControllerVelocity.magnitude > activationThreshold);

        if (mainCamera == null)
        {
            return;
        }

        Vector3 toSocket = (transform.position - mainCamera.transform.position).normalized;
        float dot = Vector3.Dot(mainCamera.transform.forward, toSocket);

        // Check if player is looking at the crafting station
        if (dot <= lookThreshold)
        {
            if (_isCrafting) CancelCrafting();
            return;
        }

        if ((_leftControllerVelocity.magnitude > activationThreshold ||
              _rightControllerVelocity.magnitude > activationThreshold) && _potentialInteractableObject != null)
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
        UpdateProgressVisuals();
        loadingScreen.SetActive(true);
        craftingEffect.Play();
        Debug.Log("Crafting started");
    }

    void CancelCrafting()
    {
        _isCrafting = false;
        _craftingProgress = 0f;
        UpdateProgressVisuals();
        loadingScreen.SetActive(false);
        craftingEffect.Stop();
        Debug.Log("Crafting canceled");
    }

    void UpdateCraftingProcess()
    {
        if (!_isCrafting) return;

        _craftingProgress += Time.deltaTime / craftingDuration;
        _craftingProgress = Mathf.Clamp01(_craftingProgress);
        UpdateProgressVisuals();

        if (_craftingProgress >= 1f)
        {
            CompleteCrafting();
        }
    }

    void CompleteCrafting()
    {
        _isCrafting = false;
        _craftingProgress = 0f;
        UpdateProgressVisuals();
        _potentialInteractableObject.SetActive(false);
        loadingScreen.SetActive(false);
        product.SetActive(true);
        product.transform.parent = null;
        craftingEffect.Stop();
        Debug.Log("Crafting completed");

        parent.SetActive(false);
    }

    void UpdateProgressVisuals()
    {
        if (slider != null)
        {
            slider.value = _craftingProgress;
        }
    }
}