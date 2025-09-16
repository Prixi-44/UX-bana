using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class HoverCount : MonoBehaviour
{ 
public void InteractHover(HoverEnterEventArgs args)
    {
        Debug.Log("Interact select");
    }
    public void InteractSelect(SelectEnterEventArgs args)
    {
        Debug.Log("Interact Select");
    }
}

