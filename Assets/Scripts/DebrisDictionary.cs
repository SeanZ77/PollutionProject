using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Debris Dictionary")]
public class DebrisDictionary : ScriptableObject, ISerializationCallbackReceiver
{
    public List<Debris> debris = new List<Debris>();
    public Debris placeholder;

    public Dictionary<string, Debris> dictionary = new Dictionary<string, Debris>();

    public void OnBeforeSerialize()
    {
        debris.Clear();
        foreach(var v in dictionary)
        {
            debris.Add(v.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        dictionary = new Dictionary<string, Debris>();

        for(int i = 0; i<debris.Count; i++)
        {
            dictionary.Add(debris[i].dataName, debris[i]);
        }
    }

    public Debris FindDebrisFromDataName(string dataName)
    {
        Debris returnValue;
        if (dictionary.TryGetValue(dataName, out returnValue))
        {
            return returnValue;
        }
        else return placeholder;
    }
}
