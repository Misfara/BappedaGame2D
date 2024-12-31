using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayer : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 5f; 

    private bool canFollow = true;  // Flag to control whether the camera can follow the player

    // LateUpdate is called after all Update calls
    void LateUpdate()
    {
        // Only move the camera if canFollow is true
        if (!canFollow) return;

        Vector3 targetPosition = new Vector3(player.position.x, player.position.y+4f, -10);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }

    // Public method to enable/disable camera following
    public void SetFollow(bool follow)
    {
        canFollow = follow;
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.cameraPosition;
    }

    public void SaveData(GameData data)
    {
        data.cameraPosition = this.transform.position;
    }
}

