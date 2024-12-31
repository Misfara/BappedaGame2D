using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToggle : MonoBehaviour
{
    [SerializeField] private Animator animator; // Assign your Animator in the Inspector
    [SerializeField] private string animationParameter = "IsNearby"; // Name of the Animator parameter
    [SerializeField] private Transform player; // Assign the Player GameObject here
    [SerializeField] private float detectionRange = 5f; // Range to detect the player

    private bool isPlayerNearby = false;

    void Update()
    {
        // Check distance manually (optional, if you prefer distance checks)
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            bool nearby = distance <= detectionRange;

            if (nearby != isPlayerNearby)
            {
                isPlayerNearby = nearby;
                animator.SetBool(animationParameter, isPlayerNearby);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            animator.SetBool(animationParameter, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            animator.SetBool(animationParameter, false);
        }
    }

}
