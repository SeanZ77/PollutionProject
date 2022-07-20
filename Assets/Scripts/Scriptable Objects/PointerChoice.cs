using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Pointer Choice")]
public class PointerChoice : ScriptableObject, ISerializationCallbackReceiver
{
    public Debris debris;
    public Debris defaultOption;

    public void OnAfterDeserialize()
    {
       
    }

    public void OnBeforeSerialize()
    {
        debris = defaultOption;
    }
}
