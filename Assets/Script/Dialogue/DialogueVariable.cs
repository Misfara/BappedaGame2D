
using System.Collections.Generic;
using UnityEngine;

using Unity.VisualScripting;


public class DialogueVariable 
{
    
   /* public Dictionary<string , Ink.Runtime.Object> variables {get;private set;}

    private Story globalVariableStory;
    private const string saveVariableKey = "INK_VARIABLES";

    public DialogueVariable(TextAsset loadGlobalScript)
    {
       globalVariableStory = new Story(loadGlobalScript.text);
 
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalVariableStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariableStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized " + name + " = " + value);
        }


    }


    public void StartListening (Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;

    } 

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }


    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if(variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key,variable.Value);
        }
    }*/
}
