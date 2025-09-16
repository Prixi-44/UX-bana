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
    public MoveAxis moveAxis = MoveAxis.Forward;

    [Header("Audio")]
    public AudioSource moveAudioSource; // assign an AudioSource with a looping clip
    public float minMoveThreshold = 0.01f; // sensitivity for detecting movement

    void Update()
    {
        // Read and normalize lever
        float angle = hinge.angle;
        normalizedValue = Mathf.InverseLerp(hinge.limits.min, hinge.limits.max, angle) * 2f - 1f;

        if (controlledObject != null)
        {
            Vector3 dir = Vector3.zero;
            switch (moveAxis)
            {
                case MoveAxis.Forward: dir = controlledObject.forward; break;
                case MoveAxis.Right: dir = controlledObject.right; break;
                case MoveAxis.Up: dir = controlledObject.up; break;
            }

            Vector3 move = dir * (normalizedValue * moveSpeed * Time.deltaTime);
            controlledObject.position += move;

            // 🎵 Handle movement sound
            if (moveAudioSource != null)
            {
                if (move.magnitude > minMoveThreshold)
                {
                    if (!moveAudioSource.isPlaying)
                        moveAudioSource.Play();
                }
                else
                {
                    if (moveAudioSource.isPlaying)
                        moveAudioSource.Stop();
                }
            }
        }
        else
        {
            // No controlled object → stop sound
            if (moveAudioSource != null && moveAudioSource.isPlaying)
                moveAudioSource.Stop();
        }
    }

    // UI Button hook to switch cubes
    public void SetControlledObject(Transform newTarget)
    {
        controlledObject = newTarget;
    }
}




