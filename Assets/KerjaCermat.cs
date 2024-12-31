using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KerjaCermat : MonoBehaviour
{
      public static float kinerjaTotal = 0;
      public static float thisRoundAddedKinerja = 0;
    [SerializeField] private GameObject gamePanel;
    Player player;

    [SerializeField] private Transform[] slots;  // The array of slots
    private GameObject[] slotContents; // Store the references to filled slot objects
    DrawCard drawCard;
     [SerializeField] public TextMeshProUGUI kinerjaText;

    public void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        slotContents = new GameObject[slots.Length];  // Initialize the array to store slot contents
        drawCard = FindAnyObjectByType<DrawCard>();
    }

    // Method to fill the slot with the dragged object
    public bool FillSlot(GameObject draggableObject)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slotContents[i] == null) // Check if the slot is empty
            {
                // Place the draggable object in the slot
                draggableObject.transform.SetParent(slots[i]);
                draggableObject.transform.position = slots[i].position;
                slotContents[i] = draggableObject; // Mark this slot as filled
                return true; // Successfully placed
            }
        }
        return false; // No empty slot available
    }

    // Method to reset the slot (when cancelling the placement)
    public void ResetSlot(GameObject draggableObject)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slotContents[i] == draggableObject) // Check if this slot contains the draggable object
            {
                slotContents[i] = null; // Reset the slot to be empty
                break; // Exit the loop once the slot is reset
            }
        }
    }

    // Optional: A method to get the current slot's RectTransform for matching sizes
    public RectTransform GetCurrentSlotRect()
    {
        // For this example, we'll just return the first slot's RectTransform, but you can modify this logic
        if (slots.Length > 0)
        {
            return slots[0].GetComponent<RectTransform>(); // Assumes each slot has a RectTransform
        }
        return null;
    }

     public static float GetKinerjaTotalThisRound()
    {
        return thisRoundAddedKinerja;
    }

    // Method to update kinerjaTotal (already added in your existing implementation)
    public void AddToKinerjaTotal(int amount)
    {
          
        
        kinerjaTotal += amount;
        Debug.Log($"Kinerja total updated: {kinerjaTotal}");
    }
}
