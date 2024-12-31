using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

public class DigitalClock : MonoBehaviour,IDataPersistence
{
    [SerializeField] private TextMeshProUGUI digitalClockText; 
    [SerializeField] private TextMeshProUGUI dayText; 
    [SerializeField] private float timeSpeed = 1f; 
    [SerializeField] private GameObject EndGamePanel; 
    [SerializeField] private Animator EndGamePanelAnimator;

    public bool clockCanStart = true;

    public int hours = 6;  
    public int minutes = 0; 
    private int seconds = 0; 
    public int day = 1; 
    private float elapsedTime = 0f;

    [SerializeField] private List<GameObject> backgroundObjects; // List of game objects to update
    [SerializeField] private Image panelImage; 
    private Color colorMorning = new Color(0.537f, 0.608f, 0.651f); // #899BA6
    private Color colorNoon = new Color(1f, 1f, 1f); // #FFFFFF
    private Color colorNight = new Color(0.192f, 0.102f, 0.110f); // #311A1C

 [SerializeField] private List<SpriteRenderer> originalBackgrounds; // Array of original backgrounds
[SerializeField] private List<SpriteRenderer> targetBackgrounds;  // Array of target backgrounds (SpriteRenderers)

private float fadeStartTime = 13f; // Time when the fade-out/fade-in starts (13:00)
private float fadeEndTime = 19f; // Time when the fade-out/fade-in ends (19:00)

    public void OnEnable()
    {
        Clock();
    }

    void Update()
    {
        Clock();
    }

    public void SetTime(int newHours, int newMinutes, int newSeconds)
    {
        hours = Mathf.Clamp(newHours, 0, 23);
        minutes = Mathf.Clamp(newMinutes, 0, 59);
        seconds = Mathf.Clamp(newSeconds, 0, 59);
    }

    public void SetTimeSpeed(float newTimeSpeed)
    {
        timeSpeed = Mathf.Max(newTimeSpeed, 0f); // Prevent negative speeds
    }

    public void Clock()
    {
        // Check if the clock is still allowed to update
        if (clockCanStart)
        {
            elapsedTime += Time.deltaTime * timeSpeed;

            if (elapsedTime >= 1f)
            {
                seconds += 1;
                elapsedTime -= 1f;

                if (seconds >= 60)
                {
                    seconds = 0;
                    minutes += 1;
                }

                if (minutes >= 60)
                {
                    minutes = 0;
                    hours += 1;
                }

                // If the time reaches 22:00
                if (hours == 22 && minutes == 0 && seconds == 0)
                {
                    clockCanStart = false; // Stop the clock
            
                }

                // Reset hours to 22 and seconds/minutes to 0 if time surpasses 22:00
                if (hours >= 22)
                {
                    hours = 22;
                    minutes = 0;
                    seconds = 0;
                }

                if(day>=6)
                {
                    StartCoroutine(EndGame());
                }
            }

            // Update the clock display
            if (digitalClockText != null)
            {
                digitalClockText.text = $"{hours:00}:{minutes:00}";
            }

            if (dayText != null)
            {
                dayText.text = "Hari " + day;
            }

            // Update background objects' colors
            UpdateBackgroundColors();
            UpdatePanelAlpha();
            UpdateBackgroundTransition();
        }
    }

    public void UpdateBackgroundColors()
    {
        if (backgroundObjects == null || backgroundObjects.Count == 0) return;

        // Interpolate color based on time of day
        float t = 0f; // Interpolation factor
        Color targetColor = colorMorning;

        if (hours >= 6 && hours < 11)
        {
            t = Mathf.InverseLerp(6f, 11f, hours + minutes / 60f);
            targetColor = Color.Lerp(colorMorning, colorNoon, t);
        }
        else if (hours >= 11 && hours < 20)
        {
            t = Mathf.InverseLerp(11f, 20f, hours + minutes / 60f);
            targetColor = Color.Lerp(colorNoon, colorNight, t);
        }
        else if (hours >= 20 || hours < 6)
        {
            if (hours >= 20) t = Mathf.InverseLerp(20f, 24f, hours + minutes / 60f);
            else t = Mathf.InverseLerp(0f, 6f, hours + minutes / 60f);
            targetColor = Color.Lerp(colorNight, colorMorning, t);
        }

        // Apply the interpolated color to each background object
         foreach (var obj in backgroundObjects)
    {
        if (obj.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
        {
            // Update color for SpriteRenderer
            spriteRenderer.color = targetColor;
        }
        
    }
    }

    public void UpdatePanelAlpha()
    {
        if (panelImage == null) return;

        // Calculate time in decimal (e.g., 14:30 becomes 14.5)
        float currentTime = hours + (minutes / 60f);

        // Interpolate alpha between 14:00 (0 alpha) and 20:00 (0.5 alpha)
        float alpha = 0f;
        if (currentTime >= 14f && currentTime <= 20f)
        {
            alpha = Mathf.Lerp(0f, 0.5f, Mathf.InverseLerp(14f, 20f, currentTime));
        }
        else if(currentTime >=20 &&  currentTime <=22 )
        {
            alpha =0.5f;
        } 

        // Apply alpha to the panel
        Color panelColor = panelImage.color;
        panelColor.a = alpha;
        panelImage.color = panelColor;
    }

public void UpdateBackgroundTransition()
{
    // Ensure the original and target backgrounds lists are initialized
    if (originalBackgrounds == null || originalBackgrounds.Count == 0 || targetBackgrounds == null || targetBackgrounds.Count == 0) return;

    // Calculate the current time in hours as a float
    float currentTime = hours + (minutes / 60f);

    // If the current time is exactly 6:00, set alpha values manually
    if (hours == 6)
    {
        // Set the original background to fully visible
        foreach (var original in originalBackgrounds)
        {
            if (original != null)
            {
                Color originalColor = original.color;
                originalColor.a = 1f; // Fully visible
                original.color = originalColor;
            }
        }

        // Set the target background to fully transparent
        foreach (var target in targetBackgrounds)
        {
            if (target != null)
            {
                Color targetColor = target.color;
                targetColor.a = 0f; // Fully transparent
                target.color = targetColor;
            }
        }
    }
    else if (currentTime >= fadeStartTime && currentTime <= fadeEndTime)
    {
        // Calculate the interpolation factor (progress) based on time within the fade range
        float t = Mathf.InverseLerp(fadeStartTime, fadeEndTime, currentTime);

        // Fade out the original backgrounds (decreasing alpha)
        foreach (var original in originalBackgrounds)
        {
            if (original != null)
            {
                Color originalColor = original.color;
                originalColor.a = 1f - t; // Decrease alpha as time progresses
                original.color = originalColor;
            }
        }

        // Fade in the target backgrounds (increasing alpha)
        foreach (var target in targetBackgrounds)
        {
            if (target != null)
            {
                Color targetColor = target.color;
                targetColor.a = t; // Increase alpha as time progresses
                target.color = targetColor;
            }
        }
    }
    else if (currentTime > fadeEndTime)
    {
        // After 19:00, ensure all backgrounds are fully the target background
        foreach (var target in targetBackgrounds)
        {
            if (target != null)
            {
                Color targetColor = target.color;
                targetColor.a = 1f; // Fully visible after the transition
                target.color = targetColor;
            }
        }

        // Ensure the original backgrounds are fully transparent
        foreach (var original in originalBackgrounds)
        {
            if (original != null)
            {
                Color originalColor = original.color;
                originalColor.a = 0f; // Fully transparent after the transition
                original.color = originalColor;
            }
        }
    }
    else if (currentTime < fadeStartTime)
    {
        // Before 13:00, ensure the original backgrounds are fully visible
        foreach (var original in originalBackgrounds)
        {
            if (original != null)
            {
                Color originalColor = original.color;
                originalColor.a = 1f; // Fully visible before the fade starts
                original.color = originalColor;
            }
        }

        // Ensure the target backgrounds are fully transparent before the fade starts
        foreach (var target in targetBackgrounds)
        {
            if (target != null)
            {
                Color targetColor = target.color;
                targetColor.a = 0f; // Fully transparent before the fade starts
                target.color = targetColor;
            }
        }
    }
}
    public void SaveData(GameData data)
    {
        data.hours = this.hours;
        data.minutes = this.minutes;
        data.seconds = this.seconds;
        data.day = this.day;
    }

      public void LoadData(GameData data)
    {
          this.hours = data.hours;
        this.minutes = data.minutes;
      
        this.seconds = data.seconds;
        this.day = data.day;
    }

    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3f);
        EndGamePanel.SetActive(true);
        EndGamePanelAnimator.SetBool("EndGame",true);
        
        
    }

    public IEnumerator OnButtonEndGameClicked()
    {
        EndGamePanelAnimator.SetBool("EndGame",false);
        yield return new WaitForSeconds(1f);
        EndGamePanelAnimator.SetTrigger("Done");
        EndGamePanel.SetActive(false);
        yield return new WaitForSeconds(0f);
    }
}
