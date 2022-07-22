using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class ToggleChoice : ScriptableObject, ISerializationCallbackReceiver
{
    public List<string> choice = new List<string>();

    //clears out choices after simulation ends
    public void OnAfterDeserialize()
    {
        choice.Clear();
    }

    public void OnBeforeSerialize()
    {
        
    }
}
