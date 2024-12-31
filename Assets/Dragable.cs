using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Required for Image components

public class Draggable : MonoBehaviour
{
  
    public KerjaCermat kerjaCermat;
    public Button button;
    public Collider2D collide; // Renamed from collider2D to collide

    // Store the original position and size to revert back to when canceled
    public Vector3 originalPosition;
    public Vector2 originalSize;
    public Transform originalParent;

    public bool isPlaced = false; // Track whether the object has already been placed in a slot

     public EnergyCost energyCost;
    private ICard currentCard;

    private StopWatch stopwatch;

   
    
    public void Start()
    {
       
         currentCard = GetComponent<ICard>();
         if (currentCard == null)
        {
            Debug.LogError("No ICard component found on this object.");
        }
        energyCost = FindAnyObjectByType<EnergyCost>();
       
        kerjaCermat = GameObject.FindObjectOfType<KerjaCermat>();

        // Try to get Button and Collider2D components (if applicable)
        button = GetComponent<Button>();
        collide = GetComponent<Collider2D>(); // Using renamed 'collide'

        // Store the initial position and size of the draggable object
        originalPosition = transform.position;
        originalSize = GetComponent<RectTransform>().sizeDelta;
       originalParent = transform.parent;

       stopwatch= FindAnyObjectByType<StopWatch>();

    }

    public void OnButtonClicked()
    {
        if(stopwatch.elapsedTime <= 0)
        {
            return;
        }
         if (currentCard == null)
        {
            Debug.LogError("Current card is null.");
            return;
        }

        // Check if energy is sufficient
        if (!currentCard.CanPlay(energyCost.energy))
        {
            Debug.Log("Not enough energy to play this card.");
            return;
        }

        // Reduce energy and play the card
        AudioManager.Instance.PlaySFX("MouseClick");
        currentCard.Play();

        // Call the FillSlot method on KerjaCermat with this object
       
        bool success = kerjaCermat.FillSlot(gameObject);

        if (success )
        {
            Debug.Log($"{gameObject.name} has been added to a slot.");

            // Get the RectTransform of this object and the target slot
            RectTransform draggableRect = GetComponent<RectTransform>();
            RectTransform slotRect = kerjaCermat.GetCurrentSlotRect();

            if (draggableRect != null && slotRect != null)
            {
                // Match the dimensions of the draggable object to the slot
                draggableRect.sizeDelta = slotRect.sizeDelta;
            }

            // Disable the Button component to make it unclickable (if using UI buttons)
            if (button != null)
            {
                button.interactable = false;
            }

            // Disable the Collider2D component to prevent physical interactions (if using colliders)
            if (collide != null)
            {
                collide.enabled = false;
            }

            // Optionally, disable the entire Draggable script (no more dragging)
            this.enabled = false;

            isPlaced = true; // Mark as placed
            
        }
        else
        {
            Debug.Log($"{gameObject.name} could not be added to a slot.");
        }
    }

   


}
