using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestingBlock : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string objectID;
    [ContextMenu("Generate guid for id")]
    private void GenerateBuild()
    {
        objectID = System.Guid.NewGuid().ToString();
    }

    [SerializeField] public TextMeshProUGUI gold;
    private static int totalGold = 0;  // Shared across all instances
    private bool isCollected = false;

    void Start()
    {
        UpdateGoldText();  // Initialize the gold UI text
    }

    public void LoadData(GameData data)
    {
        // Load total gold from saved data
        totalGold = data.totalGold;

        // Check if this gold block has already been collected
        if (data.goldCollected.TryGetValue(objectID, out isCollected) && isCollected)
        {
            Destroy(gameObject);  // This gold block was already collected
        }

        // Update UI with the loaded total gold
        UpdateGoldText();
    }

    public void SaveData(GameData data)
    {
        // Save the collected status and current gold amount
        if (data.goldCollected.ContainsKey(objectID))
        {
            data.goldCollected.Remove(objectID);
        }
        data.goldCollected.Add(objectID, isCollected);

        // Save the total gold count
        data.totalGold = totalGold;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            totalGold += 1;  // Increment global gold count
            UpdateAllGoldText();  // Ensure all instances update their gold UI
            isCollected = true;  // Mark this block as collected
            Destroy(gameObject);  // Destroy the gold block after collection
        }
    }

    // Update this instance's gold UI text
    private void UpdateGoldText()
    {
        if (gold != null)
        {
            gold.text =  totalGold.ToString();
        }
    }

    // Update all instances' gold UI text
    private void UpdateAllGoldText()
    {
        // Find all objects in the scene that have a TextMeshProUGUI component
        TestingBlock[] golds = FindObjectsOfType<TestingBlock>();
        foreach (var gold in golds)
        {
            gold.UpdateGoldText();  // Update each block's gold text
        }
    }
}
