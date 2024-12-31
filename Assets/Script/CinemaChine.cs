using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaChine : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform player;
    
    // Optional variable to control how much to offset Y
    [SerializeField] private float yOffset = 2f;
    
    private CinemachineTransposer transposer;
    
    void Start()
    {
        // Get the Transposer component from the Virtual Camera
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }
    
    void Update()
    {
        // Dynamically adjust the camera's Y position offset
        Vector3 followOffset = transposer.m_FollowOffset;
        
        // Example: Increase Y position dynamically by yOffset
        followOffset.y = player.position.y + yOffset;
        
        // Set the new offset
        transposer.m_FollowOffset = followOffset;
    }
}
