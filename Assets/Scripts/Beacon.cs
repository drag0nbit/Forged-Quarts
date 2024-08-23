using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    public bool active;
    private TooltipManager tooltipManager;

    void Start()
    {
        tooltipManager = FindObjectOfType<TooltipManager>(); // Find the tooltip manager in the scene
    }

    void OnMouseEnter()
    {
        if (!active) tooltipManager.ShowTooltip("beacon", "Кликните чтобы активировать");
    }

    void OnMouseExit()
    {
        tooltipManager.HideTooltip();
    }

    public void Interact() 
    {
        // Change the color of all child SpriteRenderer components to white
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = Color.white;
        }
        active = true;
        tooltipManager.HideTooltip();
    }
}
