using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Debris Dictionary")]
public class DebrisDictionary : ScriptableObject
{
    public List<Debris> debris = new List<Debris>();
    public Debris placeholder;

    public Debris FindDebrisFromDataName(string dataName)
    {
        foreach (Debris d in debris) {
            Debug.Log(dataName);
            if (dataName == d.dataName) {
                return d;
            }
        }
        return placeholder;
    }
}
