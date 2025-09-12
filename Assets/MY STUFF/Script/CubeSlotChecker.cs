using UnityEngine;

public class CubeSlotChecker : MonoBehaviour
{
    [Header("Expected Cube Tag")]
    public string requiredTag; // e.g. "CubeA"

    [Header("Slot Visual")]
    public Renderer slotRenderer;
    public string successHexColor = "#04FF00";  // Green
    public string wrongHexColor = "#FF0000";  // Red
    public string defaultHexColor = "#00E9FF";  // Blue

    private Color successColor;
    private Color wrongColor;
    private Color defaultColor;

    private bool cubeInside = false;

    void Start()
    {
        // Parse hex strings to Unity colors
        ColorUtility.TryParseHtmlString(successHexColor, out successColor);
        ColorUtility.TryParseHtmlString(wrongHexColor, out wrongColor);
        ColorUtility.TryParseHtmlString(defaultHexColor, out defaultColor);

        if (slotRenderer != null)
            slotRenderer.material.color = defaultColor;
    }

    void OnTriggerStay(Collider other)
    {
        if (slotRenderer == null) return;

        // Only react to cubes
        if (other.CompareTag("CubeA") || other.CompareTag("CubeB") || other.CompareTag("CubeC"))
        {
            Bounds triggerBounds = GetComponent<Collider>().bounds;
            Bounds cubeBounds = other.bounds;

            if (triggerBounds.Contains(cubeBounds.min) && triggerBounds.Contains(cubeBounds.max))
            {
                // Cube is fully inside
                cubeInside = true;

                if (other.CompareTag(requiredTag))
                {
                    // Correct cube fully inside
                    slotRenderer.material.color = successColor;
                }
                else
                {
                    // Wrong cube fully inside
                    slotRenderer.material.color = wrongColor;
                }
            }
            else
            {
                // Cube is only partially inside → stay default
                cubeInside = false;
                slotRenderer.material.color = defaultColor;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (slotRenderer == null) return;

        if (other.CompareTag("CubeA") || other.CompareTag("CubeB") || other.CompareTag("CubeC"))
        {
            cubeInside = false;
            slotRenderer.material.color = defaultColor;
        }
    }
    public bool IsCorrect()
    {
        return slotRenderer != null && slotRenderer.material.color == successColor;
    }
}

