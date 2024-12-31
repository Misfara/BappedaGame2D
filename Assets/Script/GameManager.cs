using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private HashSet<string> destroyedObjects = new HashSet<string>();

    void Awake()
    {
        // Make sure the GameManager persists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddDestroyedObject(string objectID)
    {
        destroyedObjects.Add(objectID);
    }

    public bool IsObjectDestroyed(string objectID)
    {
        return destroyedObjects.Contains(objectID);
    }
}

