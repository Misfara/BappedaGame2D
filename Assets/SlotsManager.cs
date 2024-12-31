using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WildSlotManager : MonoBehaviour
{
    [SerializeField] private Transform[] wildSlots; // Wild slots to place the items back (same as before)
    private KerjaCermat kerjaCermat; // Reference to KerjaCermat to reset its slots
    private EnergyCost energyCost;

    private RapatCard rapatCard;
    private TahunAnggaranCard tahunAnggaranCard;
    public void Start()
    {
        kerjaCermat = GameObject.FindObjectOfType<KerjaCermat>(); // Find KerjaCermat in the scene
        energyCost = FindAnyObjectByType<EnergyCost>();
        tahunAnggaranCard = FindAnyObjectByType<TahunAnggaranCard>();
        rapatCard = FindAnyObjectByType<RapatCard>();
    }

    // Method to reset all placed draggable objects and return them to their original position in the wild slots
    public void OnCancelButtonClicked()
    {
        // Find all draggable objects in the scene
        Draggable[] allDraggables = FindObjectsOfType<Draggable>();

        foreach (var draggable in allDraggables)
        {
            // Only reset the placed draggable objects (skip if not placed)
            if (draggable.isPlaced)
            {
                // Reset the size and position of the draggable object to its original state
                ResetDraggable(draggable);

                // Reset the KerjaCermat slot where the draggable object was placed
                kerjaCermat.ResetSlot(draggable.gameObject);

                // Reset the parent of the draggable to the wild slot (or its default position)
                ResetParentToWildSlot(draggable);

                // Enable the Button and Collider again if you want it to be draggable
                if (draggable.button != null)
                {
                    draggable.button.interactable = true;
                }

                if (draggable.collide != null)
                {
                    draggable.collide.enabled = true;
                }

                draggable.enabled = true;

                // After canceling, reset placement status
                draggable.isPlaced = false; // Reset placement flag

                
            }
            energyCost.ResetEnergy();
            
        }
        
        
      
        
    }

    // Helper method to reset the draggable object size and position
    private void ResetDraggable(Draggable draggable)
    {
        // Reset the size back to its original size
        RectTransform draggableRect = draggable.GetComponent<RectTransform>();
        if (draggableRect != null)
        {
            draggableRect.sizeDelta = draggable.originalSize;
        }

        // Reset the position to its original position
        draggable.transform.position = draggable.originalPosition;
    }

    // Helper method to reset the parent of the draggable to the first available wild slot
    private void ResetParentToWildSlot(Draggable draggable)
    {
        // Find the first empty wild slot to place the draggable object back
        foreach (Transform wildSlot in wildSlots)
        {
            if (wildSlot.childCount == 0) // Find an empty slot
            {
                draggable.transform.position = draggable.originalPosition;   // Position it at the wild slot
                draggable.transform.SetParent(draggable.originalParent);             // Set the draggable object as a child of the wild slot
                draggable.isPlaced = true;                            // Mark as placed

                break; // Exit after placing the object in the first available wild slot
            }
        }
    }

}
