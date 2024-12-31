using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;  // Singleton instance

    public TextMeshProUGUI goldText;  // Reference to the UI element showing gold count
    private int totalGold = 0;  // Tracks global gold count

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep the manager between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Adds to the total gold count and updates the UI
    public void AddGold(int amount)
    {
        totalGold += amount;
        UpdateGoldText();
    }

    // Updates the gold text in the UI
    private void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = "Gold: " + totalGold.ToString();
        }
    }

    public int GetGoldAmount()
    {
        return totalGold;
    }
}
