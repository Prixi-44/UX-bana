using UnityEngine;

public class LeverController : MonoBehaviour
{
    [Header("Lever Setup")]
    public HingeJoint hinge;   // Assign in inspector
    [Range(-1f, 1f)]
    public float normalizedValue; // -1 = full back, +1 = full forward

    [Header("Controlled Object")]
    public Transform controlledObject;   // The current object to move
    public float moveSpeed = 5f;         // Movement speed

    void Update()
    {
        // Read lever angle & normalize it (-1 to 1)
        float angle = hinge.angle;
        normalizedValue = Mathf.InverseLerp(hinge.limits.min, hinge.limits.max, angle) * 2f - 1f;

        // Move the active object if assigned
        if (controlledObject != null)
        {
            Vector3 move = controlledObject.forward * (normalizedValue * moveSpeed * Time.deltaTime);
            controlledObject.position += move;
        }
    }

    // Called by UI buttons to switch targets
    public void SetControlledObject(Transform newTarget)
    {
        controlledObject = newTarget;
    }
}

