using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class CountInteractionSystem : MonoBehaviour
{
    // Visa pilarna endast f�rst interactable (true) eller
    // varenda g�ng vi interagerar med n�got nytt interactable (false)
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

        // Skriver ut namnet p� det objekt som du interagerar med
        // n�r du hovrar med nearfar interactor �ver den. 
        string interactableName = args.interactableObject.transform.gameObject.name;

        // Kolla om pilerna ska visas f�r detta objekt
        // dvs om vi hade interagerat med objektet en g�ng
        // via select is�fall visas inte f�r just detta objekt

        // om det �r n�got annat objekt och vi har "noll" g�nger
        // interagerat med select, visa animationen f�r pilerna f�r detta interactable

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

        // S� fort vi interagerar med objektet
        // ska detta reggas f�rsta g�ngen. 
        _trackedInteractables[interactableName]++;
    }
}

