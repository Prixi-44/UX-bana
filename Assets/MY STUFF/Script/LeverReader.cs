using UnityEngine;

public class LeverReader : MonoBehaviour
{
    public HingeJoint hinge;   // Assign in inspector
    public float normalizedValue; // -1 to 1 range

    void Update()
    {
        float angle = hinge.angle; // degrees
        normalizedValue = Mathf.InverseLerp(hinge.limits.min, hinge.limits.max, angle) * 2f - 1f;

        // Debug
        Debug.Log($"Lever angle: {angle}, Value: {normalizedValue}");
    }
}

