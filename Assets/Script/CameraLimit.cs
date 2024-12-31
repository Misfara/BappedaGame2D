using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraStaticZone : MonoBehaviour
{
    private Vector3 fixedCameraPosition;  // Variable to store the camera's position
    public FollowingPlayer cameraFollow;  // Reference to the FollowingPlayer script
     private Transform player;
    [SerializeField] private Transform mainCamera;

    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           

            cameraFollow.SetFollow(false);

         
            mainCamera.position = this.mainCamera.position;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Save the current camera position and make it static
            

            // Disable the camera following behavior
            cameraFollow.SetFollow(false);

            // Keep the camera at the fixed position
            mainCamera.position = this.mainCamera.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Re-enable camera movement when the player exits the trigger zone
            cameraFollow.SetFollow(true);
        }
    }
}



