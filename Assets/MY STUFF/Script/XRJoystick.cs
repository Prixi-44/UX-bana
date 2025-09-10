using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRGrabInteractable))]
public class XRJoystick : MonoBehaviour
{
    public enum AxisOptions { Both, HorizontalOnly, VerticalOnly }

    [Header("References")]
    public Transform handle; // assign this in Inspector

    [Header("Settings")]
    public AxisOptions axis = AxisOptions.Both;
    public float maxAngle = 30f;
    public bool recenterOnRelease = true;

    [Header("Output")]
    public Vector2 outputValue;
    public UnityEvent<float> onXValueChanged;
    public UnityEvent<float> onYValueChanged;

    private Quaternion _initialLocalRotation;
    private XRBaseInteractor _interactor;

    void Start()
    {
        _initialLocalRotation = handle.localRotation;

        var interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnGrabbed);
        interactable.selectExited.AddListener(OnReleased);
    }

    void OnDestroy()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.RemoveListener(OnGrabbed);
        interactable.selectExited.RemoveListener(OnReleased);
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        _interactor = args.interactorObject as XRBaseInteractor;
    }

    void OnReleased(SelectExitEventArgs args)
    {
        _interactor = null;
        if (recenterOnRelease)
        {
            outputValue = Vector2.zero;
            handle.localRotation = _initialLocalRotation;
            onXValueChanged.Invoke(0f);
            onYValueChanged.Invoke(0f);
        }
    }

    void Update()
    {
        if (_interactor == null) return;

        // Get direction from joystick to interactor
        Vector3 localDirection = transform.InverseTransformPoint(_interactor.GetAttachTransform(null).position) - handle.localPosition;

        float x = Mathf.Clamp(localDirection.x, -1, 1);
        float y = Mathf.Clamp(localDirection.z, -1, 1); // using z for vertical because of joystick orientation

        if (axis == AxisOptions.HorizontalOnly) y = 0;
        if (axis == AxisOptions.VerticalOnly) x = 0;

        Vector2 input = new Vector2(x, y);
        input = Vector2.ClampMagnitude(input, 1f);

        outputValue = input;

        ApplyRotationToHandle(input);
        onXValueChanged.Invoke(outputValue.x);
        onYValueChanged.Invoke(outputValue.y);
    }

    void ApplyRotationToHandle(Vector2 input)
    {
        Quaternion targetRotation = Quaternion.Euler(
            -input.y * maxAngle, // pitch
            0,
            -input.x * maxAngle  // roll
        );
        handle.localRotation = targetRotation;
    }
}
