using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAudioInfo", menuName = "ScriptableObjects/DialogueAudioInfoSO", order = 1)]
public class DialogueAudioInfoSO : ScriptableObject
{   
    public string id;

    public  AudioClip[] dialogueTypingSoundClips;
    [Range(1,5)]
    public int frequencyLevel = 2;
    [Range(-3,3)]
    public float maxPitch = 3f;
    [Range(-3,3)]
    public float minPitch = 0.5f;
    public bool stopAudioSource;

     [Range(0f, 1f)]
    public float volume = 1f; // Default set to maximum volume
}
