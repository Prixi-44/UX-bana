using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ConfigurableJoint))]
public class VRButton : MonoBehaviour
{
    [Header("Button Settings")]
    public float pressThreshold = 0.015f; // close to limit (e.g. 0.02)
    public float releaseThreshold = 0.01f; // when to allow re-press

    [Header("Events")]
    public UnityEvent onButtonPressed;

    private ConfigurableJoint joint;
    private Vector3 initialLocalPosition;
    private bool isPressed;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
        initialLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        float displacement = initialLocalPosition.y - transform.localPosition.y;

        if (!isPressed && displacement >= pressThreshold)
        {
            isPressed = true;
            onButtonPressed?.Invoke();
        }
        else if (isPressed && displacement < releaseThreshold)
        {
            isPressed = false;
        }
    }
}
