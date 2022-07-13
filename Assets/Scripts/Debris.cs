using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Debris")]
public class Debris : ScriptableObject
{
    public string name;
    public string dataName;
    public string description;
    public Sprite image;
    public Sprite icon;
    public Color color;
}
