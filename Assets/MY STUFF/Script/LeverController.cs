using UnityEngine;

public class LeverController : MonoBehaviour
{
    public enum MoveAxis { Forward, Right, Up }

    [Header("Lever Setup")]
    public HingeJoint hinge;
    [Range(-1f, 1f)]
    public float normalizedValue;

    [Header("Controlled Object")]
    public Transform controlledObject;
    public float moveSpeed = 5f;
    public MoveAxis moveAxis = MoveAxis.Forward; // choose in Inspector

    void Update()
    {
        // Read & normalize hinge angle
        float angle = hinge.angle;
        normalizedValue = Mathf.InverseLerp(hinge.limits.min, hinge.limits.max, angle) * 2f - 1f;

        // Apply movement
        if (controlledObject != null)
        {
            Vector3 dir = Vector3.zero;

            switch (moveAxis)
            {
                case MoveAxis.Forward: dir = controlledObject.forward; break;
                case MoveAxis.Right: dir = controlledObject.right; break;
                case MoveAxis.Up: dir = controlledObject.up; break;
            }

            controlledObject.position += dir * (normalizedValue * moveSpeed * Time.deltaTime);
        }
    }

    // 🔥 This lets UI buttons switch what object is controlled
    public void SetControlledObject(Transform newTarget)
    {
        controlledObject = newTarget;
    }
}



