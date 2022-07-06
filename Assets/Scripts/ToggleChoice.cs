using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class ToggleChoice : ScriptableObject
{
    public Debris[] types;
    public string choice;
    public string previousChoice;
}
