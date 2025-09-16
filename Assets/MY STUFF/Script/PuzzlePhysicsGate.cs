using UnityEngine;

public class PuzzlePhysicsGate : MonoBehaviour
{
    private Rigidbody[] rbs;

    void Awake()
    {
        // Get ALL Rigidbodies in children (even children of children)
        rbs = GetComponentsInChildren<Rigidbody>(includeInactive: true);
    }

    void OnEnable()
    {
        // Freeze all rigidbodies at the start
        SetKinematic(true);
    }

    // 🔥 Call this with an Animation Event at the end of your rise animation
    public void EnablePhysics()
    {
        Debug.Log("✅ EnablePhysics() called - unfreezing rigidbodies");
        SetKinematic(false);
    }

    private void SetKinematic(bool state)
    {
        foreach (var rb in rbs)
        {
            rb.isKinematic = state;
        }
    }
}
