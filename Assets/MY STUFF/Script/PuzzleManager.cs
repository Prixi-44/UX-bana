using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    [Header("Slots to Check")]
    public CubeSlotChecker[] slots; // Drag your 3 slot scripts here

    [Header("Events")]
    public UnityEvent onAllCorrect; // Fires once when all slots are green

    private bool puzzleSolved = false;

    void Update()
    {
        if (!puzzleSolved && AllSlotsCorrect())
        {
            puzzleSolved = true;
            Debug.Log("Puzzle Solved!");
            onAllCorrect.Invoke();
        }
    }

    private bool AllSlotsCorrect()
    {
        foreach (var slot in slots)
        {
            if (!slot.IsCorrect())
                return false;
        }
        return true;
    }
}

