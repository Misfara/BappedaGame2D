using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    public FollowingPlayer cameraFollow;  // Reference to the FollowingPlayer script
    private Transform player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform bappeda;
    [SerializeField] private float followSpeed = 50f;

    private bool startFollowing = false;
    private bool isZoomingOut = false;  // Flag to check if zooming out
    private bool isZoomingIn = false;  // Flag to check if zooming in

    private float zoomSpeed = 2f;  // Adjust this for smooth zoom speed
    private float targetOrthographicSize = 15f;  // Target zoom size for zoom out
    private float defaultOrthographicSize = 6.9f;  // Default zoom size for zoom in
    private float currentZoom;  // Current zoom level

    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        currentZoom = defaultOrthographicSize;  // Initialize with the current camera zoom
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start zooming out only when entering the trigger
            isZoomingOut = true;
            isZoomingIn = false;  // Make sure zooming in is disabled
            cameraFollow.SetFollow(false);  // Disable camera follow
            startFollowing = true;
            StartCoroutine(ChangingCamera());
            // Set target orthographic size to 17 for zooming out
            targetOrthographicSize = 15f;
            AudioManager.Instance.PlaySFX("ZoomOutBappeda");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start zooming in only when exiting the trigger
            isZoomingIn = true;
            isZoomingOut = false;  // Make sure zooming out is disabled
            cameraFollow.SetFollow(true);  // Re-enable camera follow
            startFollowing = false;
            // Set target orthographic size to 6.9 for zooming in
            targetOrthographicSize = defaultOrthographicSize;
             
        }
    }

    void Update()
    {
        if (isZoomingOut)
        {
            // Zoom out smoothly only when flag is true
            currentZoom = Mathf.Lerp(currentZoom, targetOrthographicSize, zoomSpeed * Time.deltaTime);
        }
        else if (isZoomingIn)
        {
            // Zoom in smoothly only when flag is true
            currentZoom = Mathf.Lerp(currentZoom, targetOrthographicSize, zoomSpeed * Time.deltaTime);
        }

        // Apply the updated zoom to the camera
        mainCamera.orthographicSize = currentZoom;
    }

    private IEnumerator ChangingCamera()
    {
        // Start from the player's position
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 endPosition = new Vector3(bappeda.transform.position.x, bappeda.transform.position.y + 10f, -10);
        float journeyLength = Vector3.Distance(startPosition, endPosition);
        float startTime = Time.time;

        // Smoothly transition to Bappeda's position
        while (Vector3.Distance(mainCamera.transform.position, endPosition) > 0.05f)
        {
            float distanceCovered = (Time.time - startTime) * followSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            mainCamera.transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

            yield return null;
        }

        // Ensure the camera reaches the exact target position
        mainCamera.transform.position = endPosition;
    }
}
