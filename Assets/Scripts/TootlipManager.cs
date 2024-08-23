using UnityEngine;
using TMPro;
using System.Reflection;

public class TooltipManager : MonoBehaviour
{
    public TextMeshProUGUI tooltipText; // Reference to the TextMeshProUGUI component
    public TextMeshProUGUI subTooltipText; // Reference to the TextMeshProUGUI component
    public GameObject tooltipObject; // The parent GameObject for the tooltip
    private Camera mainCamera;
    private bool isInteracting;
    public bool tooltipShown;

    void Start()
    {
        mainCamera = Camera.main;
        tooltipObject.SetActive(false); // Hide tooltip initially
        isInteracting = false;
        tooltipShown = false;
    }

    void Update()
    {
        // Move tooltip to follow the mouse
        Vector3 mousePos = Input.mousePosition;
        tooltipObject.transform.position = mousePos;

        // Check for mouse click
        if (Input.GetMouseButtonDown(0) && !isInteracting)
        {
            isInteracting = true; // Set the flag to true

            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            
            if (hit.collider != null)
            {
                // Get all MonoBehaviour components on the hit object
                MonoBehaviour[] interactables = hit.collider.GetComponents<MonoBehaviour>();
                
                foreach (var interactable in interactables)
                {
                    MethodInfo method = interactable.GetType().GetMethod("Interact");
                    if (method != null)
                    {
                        method.Invoke(interactable, null);
                    }
                }
            }
        }

        // Reset the flag when the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isInteracting = false;
        }
    }

    public void ShowTooltip(string content, string subContent)
    {
        tooltipText.text = content;
        subTooltipText.text = subContent;
        tooltipObject.SetActive(true);
        tooltipShown = true;
    }

    public void HideTooltip()
    {
        tooltipObject.SetActive(false);
        tooltipShown = false;
    }
}
