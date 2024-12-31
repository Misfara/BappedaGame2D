using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DrawCard : MonoBehaviour
{
    public List<GameObject> deck = new List<GameObject>(); // List of cards
    public List<GameObject> discardPile = new List<GameObject>();
    public Transform[] cardSlots; // Slots where cards will be placed
    public bool[] availableCardSlots; // Available slots to place cards

    public TextMeshProUGUI deckText; // UI Text to show the deck count
     [SerializeField] public TextMeshProUGUI discardPileText;
      private List<RapatCard> rapatCards = new List<RapatCard>(); // List of RapatCard instances
    private List<TahunAnggaranCard> tahunAnggaranCards = new List<TahunAnggaranCard>();

    [SerializeField] private Animator drawAnimator;
    [SerializeField] private Animator discardAnimator;
    [SerializeField] private GameObject recycleCardPanel;
     [SerializeField] private Animator recyclingCardAnimator;
    

    public void Start()
    {
        rapatCards.AddRange(FindObjectsOfType<RapatCard>());
        tahunAnggaranCards.AddRange(FindObjectsOfType<TahunAnggaranCard>());
        
            
        
        deckText.text = deck.Count.ToString();

        discardPileText.text = discardPile.Count.ToString();
    }

    public void DrawingCard()
    {
        // Check if there are any cards in the deck
        if (deck.Count >= 1)
        {
           foreach (var card in rapatCards)
            {
                card.animator.SetBool("FadingOut", false);
            }

            foreach (var card in tahunAnggaranCards)
            {
                card.animator.SetBool("FadingOut", false);
            }

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                // If the slot is available
                if (availableCardSlots[i] == true)
                {
                    // If there are still cards left in the deck
                    if (deck.Count > 0)
                    {
                        // Get a random card from the deck
                        GameObject randCard = deck[Random.Range(0, deck.Count)];

                        // Set the card as active and place it in the current slot
                        randCard.SetActive(true);
                        randCard.transform.position = cardSlots[i].position;
                       

                        // Mark the slot as filled
                        availableCardSlots[i] = false;

                        // Remove the card from the deck
                        deck.Remove(randCard);
                    }
                }
            }
        }

        // Update deck text (Optional)
        deckText.text =  deck.Count.ToString();
        if(deck.Count <= 0)
        {
            deckText.text =  "0";
        }

        if(discardPile.Count <= 0)
        {
            discardPileText.text ="0";
        }
        
       
    }

    public void ResetAvailableSlots()
{
    // Loop through all available card slots
    for (int i = 0; i < availableCardSlots.Length; i++)
    {
        // Set every slot to true, making them available
        availableCardSlots[i] = true;
    }

    // Optional: Log the reset for debugging purposes
    Debug.Log("All slots are now available.");
}

public IEnumerator RecycleCard()
{
    if (deck.Count <= 0)
    {
        if (discardPile.Count > 0)
        {
            while (discardPile.Count > 0)
            {
                recycleCardPanel.SetActive(true);
                 recyclingCardAnimator.SetBool("Active",true);
                discardAnimator.SetBool("TakingCard",true);
                 drawAnimator.SetBool("TakingCard",true);
                 
                
                 
                // Take the first card from the discard pile
                GameObject card = discardPile[0];

                // Add it back to the deck
                deck.Add(card);
                card.SetActive(false); // Optionally deactivate the card

                // Remove the card from the discard pile
                discardPile.RemoveAt(0);

                // Update the deck and discard pile UI text
                deckText.text = deck.Count.ToString();
                discardPileText.text = discardPile.Count.ToString();

                // Wait for 0.2 seconds before proceeding to the next card
                yield return new WaitForSeconds(0.2f);
            }

            ShuffleDeck(); // Shuffle if necessary
            Debug.Log("Deck has been recycled with cards from the discard pile.");
        }
        else
        {
            Debug.Log("No cards in discard pile to recycle.");
        }
    }
    yield return new WaitForSeconds(0.5f);
    discardAnimator.SetBool("TakingCard",false);
    drawAnimator.SetBool("TakingCard",false);
    recyclingCardAnimator.SetBool("Active",false);
    recycleCardPanel.SetActive(false);

    yield return null; // Yield to ensure the coroutine completes properly
}


private void ShuffleDeck()
{
    // Simple Fisher-Yates shuffle algorithm for shuffling the deck
    for (int i = 0; i < deck.Count; i++)
    {
        GameObject temp = deck[i];
        int randomIndex = Random.Range(i, deck.Count);
        deck[i] = deck[randomIndex];
        deck[randomIndex] = temp;
    }
    Debug.Log("Deck has been shuffled.");
}

    
}
