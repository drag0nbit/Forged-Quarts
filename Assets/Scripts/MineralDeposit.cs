using UnityEngine;

public class MineralDeposit : MonoBehaviour
{
    public int count = 1;
    public string mineralKeyword = "quartz";
    private TooltipManager tooltipManager;

    void Start()
    {
        tooltipManager = FindObjectOfType<TooltipManager>(); // Find the tooltip manager in the scene
    }

    void OnMouseEnter()
    {
        tooltipManager.ShowTooltip(LocalizationManager.Auto(mineralKeyword)+" x"+count, "Кликните чтобы добыть");
    }

    void OnMouseExit()
    {
        // Hide tooltip when the mouse leaves
        tooltipManager.HideTooltip();
    }

    public void Interact() 
    {
        Destroy(gameObject);
        tooltipManager.HideTooltip();
    }
}
