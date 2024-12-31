
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class ResponseEvent
{
    [HideInInspector] public string name;
    [SerializeField] private UnityEvent onPickedResponse;
     [SerializeField] private UnityEvent additionalEffect;
  

    public UnityEvent OnPickedResponse => onPickedResponse;
    public UnityEvent AdditionalEffect => additionalEffect;
}
