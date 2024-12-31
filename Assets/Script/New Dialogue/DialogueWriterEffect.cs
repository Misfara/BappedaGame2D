using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DialogueWriterEffect : MonoBehaviour
{
 [SerializeField] private float audioCooldown = 0.1f; // Delay duration in seconds
private float lastAudioTime = -1; // Tracks the last time the sound was played
    [SerializeField]private float typeWriterSpeed = 50f; 

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {

        new Punctuation(new HashSet<char>(){'.', '!','?'},0.6f ),
        new Punctuation ( new HashSet<char>(){',', ';',':'},0.3f)
      
    };

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType,textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {

      
        float t = 0 ;
        int charIndex = 0 ;

        while(charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime* typeWriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0 , textToType.Length);
              

            for(int i = lastCharIndex; i<charIndex; i++)
            {
                bool  isLast = i >= textToType.Length-1;

                textLabel.text = textToType.Substring(0, i+1);
                 if (Time.time - lastAudioTime >= audioCooldown)
        {
            AudioManager.Instance.PlaySFX("NPCSounds");
            lastAudioTime = Time.time;
        }

                if(IsPunctuation(textToType[i],out float waitTime) && !isLast && !IsPunctuation(textToType[i+1],out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            
            yield return null;
        }

        textLabel.text = textToType;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach(Punctuation punctuationCategory in punctuations)
        {
            if(punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float  WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
