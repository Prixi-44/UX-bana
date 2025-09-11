using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketGroupChecker : MonoBehaviour
{
    public XRSocketInteractor[] groupASockets;
    public GameObject groupBParent;

    private bool groupBActivated = false;

    void Update()
    {
        if (!groupBActivated && AllSocketsFilled())
        {
            groupBParent.SetActive(true);
            groupBActivated = true;
        }
    }

    private bool AllSocketsFilled()
    {
        foreach (var socket in groupASockets)
        {
            if (!socket.hasSelection)  // instead of socket.selectTarget
                return false;
        }
        return true;
    }
}

