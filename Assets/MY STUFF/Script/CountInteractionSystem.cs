using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class CountInteractionSystem : MonoBehaviour
{
    // Visa pilarna endast först interactable (true) eller
    // varenda gång vi interagerar med något nytt interactable (false)
    [SerializeField]
    private bool showArrowsOnlyFirstInteractable;

    [SerializeField]
    private GameObject refToGrabArrow;

    [SerializeField]
    private GameObject refToTriggerArrow;

    private Dictionary<string, int> _trackedInteractables = new Dictionary<string, int>();

    private bool _showArrowsAgain = true;

    public void InteractHover(HoverEnterEventArgs args)
    {
        Debug.Log("Interact hover");

        // Skriver ut namnet på det objekt som du interagerar med
        // när du hovrar med nearfar interactor över den. 
        string interactableName = args.interactableObject.transform.gameObject.name;

        // Kolla om pilerna ska visas för detta objekt
        // dvs om vi hade interagerat med objektet en gång
        // via select isåfall visas inte för just detta objekt

        // om det är något annat objekt och vi har "noll" gånger
        // interagerat med select, visa animationen för pilerna för detta interactable

        if (!_trackedInteractables.ContainsKey(interactableName))
        {
            _trackedInteractables.Add(interactableName, 0);
        }

        if (_showArrowsAgain && _trackedInteractables[interactableName] < 1)
        {
            // Visa pilarna
            refToGrabArrow.SetActive(true);
            refToTriggerArrow.SetActive(true);

            if (showArrowsOnlyFirstInteractable)
            {
                _showArrowsAgain = false;
            }
        }
    }

    public void InteractHoverExit(HoverExitEventArgs args)
    {
        refToGrabArrow.SetActive(false);
        refToTriggerArrow.SetActive(false);
    }

    public void InteractSelect(SelectEnterEventArgs args)
    {
        Debug.Log("Interact select");

        string interactableName = args.interactableObject.transform.gameObject.name;

        // Så fort vi interagerar med objektet
        // ska detta reggas första gången. 
        _trackedInteractables[interactableName]++;
    }
}

