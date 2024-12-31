using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject , ISerializationCallbackReceiver

{
    public UnityEngine.Vector2 initialValue;
    public UnityEngine.Vector2 defaultValue;

    public void OnAfterDeserialize() {
        initialValue = defaultValue; 
    }

    public void OnBeforeSerialize() {

    }

}
