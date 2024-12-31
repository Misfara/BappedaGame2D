using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TargetTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fromClock;  // The clock that will change
    [SerializeField] private TextMeshProUGUI toClock;    // The clock that stays the same (set in Inspector)
    [SerializeField] private Animator animator;

    private int fromClockMinutes = 30;  // Total minutes since the start time (to manage hour overflow)
    private int fromClockHours = 7;    // Initial hour (set in Inspector to 08:00)
    private bool isUpdating = false;

    private Queue<IEnumerator> timeChangeQueue = new Queue<IEnumerator>(); // Queue for time updates
     private int targetHours = 16;      // Target hours (e.g., 16:00)
    private int targetMinutes = 0;    // Target minutes (e.g
    
    private StopWatch stopwatch;

    private void Start()
    {
        UpdateFromClockText();  // Initialize the fromClock text at the start
        stopwatch = FindAnyObjectByType<StopWatch>();
    }

    // Method to add time
    public void AddTime(int addedHours, int addedMinutes)
    {
        if (isUpdating)
        {
            timeChangeQueue.Enqueue(AddTimeSmoothly(addedHours, addedMinutes));
            return;
        }

        StartCoroutine(AddTimeSmoothly(addedHours, addedMinutes));
    }

    // Method to subtract time
    public void SubtractTime(int subtractedHours, int subtractedMinutes)
    {
        if (isUpdating)
        {
            timeChangeQueue.Enqueue(SubtractTimeSmoothly(subtractedHours, subtractedMinutes));
            return;
        }

        StartCoroutine(SubtractTimeSmoothly(subtractedHours, subtractedMinutes));
    }

    // Coroutine to add time smoothly
    private IEnumerator AddTimeSmoothly(int addedHours, int addedMinutes)
    {
        isUpdating = true;
        int totalMinutes = (addedHours * 60) + addedMinutes;

        while (totalMinutes > 0)
        {
            fromClockMinutes++;

            if (fromClockMinutes >= 60)
            {
                fromClockMinutes = 0;
                fromClockHours++;
            }

            UpdateFromClockText();
             if (fromClockHours > targetHours || (fromClockHours == targetHours && fromClockMinutes >= targetMinutes))
            {
                OnTargetTimeReached();
                break;  // Stop further updates if target time is reached
            }
            yield return new WaitForSeconds(0.02f);

            totalMinutes--;
        }

        animator.SetTrigger("clock");

        if (timeChangeQueue.Count > 0)
        {
            StartCoroutine(timeChangeQueue.Dequeue());
        }

        isUpdating = false;
    }

    // Coroutine to subtract time smoothly
    private IEnumerator SubtractTimeSmoothly(int subtractedHours, int subtractedMinutes)
{
    isUpdating = true;
    int totalMinutes = (subtractedHours * 60) + subtractedMinutes;

    // Set the limit you want to stop subtracting at (e.g., 7:00)
    int limitHours = 7;
    int limitMinutes = 30;

    while (totalMinutes > 0)
    {
        // If the time is already at or below the limit, stop subtracting
        if (fromClockHours == limitHours && fromClockMinutes == limitMinutes)
        {
            break;  // Exit the loop if we hit the limit
        }

        fromClockMinutes--;

        if (fromClockMinutes < 0)
        {
            fromClockMinutes = 59;
            fromClockHours--;
        }

        // Prevent hours from going below 0
        if (fromClockHours < 0)
        {
            fromClockHours = 0; // Set to 00:00, as it's the limit
        }

        UpdateFromClockText();
        if (fromClockHours > targetHours || (fromClockHours == targetHours && fromClockMinutes >= targetMinutes))
            {
                OnTargetTimeReached();
                break;  // Stop further updates if target time is reached
            }
        yield return new WaitForSeconds(0.02f);

        totalMinutes--;
    }

    animator.SetTrigger("clock");

    if (timeChangeQueue.Count > 0)
    {
        StartCoroutine(timeChangeQueue.Dequeue());
    }

    isUpdating = false;
}

    // Updates the fromClock TextMeshPro component to show the correct time
    private void UpdateFromClockText()
    {
        fromClock.text = string.Format("{0:D2}:{1:D2}", fromClockHours, fromClockMinutes);
    }

     private void OnTargetTimeReached()
    {
        stopwatch.OnStopwatchZero();
        
    }
}
