using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActivation : MonoBehaviour
{
    public GameObject panelToSet;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that panelToDeactivate is assigned in the Inspector
        if (panelToSet == null)
        {
            Debug.LogError("Panel to deactivate is not assigned!");
        }
    }

    // Method called when the button is pressed
    public void deactivatePanel()
    {
        // Deactivate the panel
        panelToSet.SetActive(false);
    }

    public void activatePanel()
    {
        panelToSet.SetActive(true);
    }
}
