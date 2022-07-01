using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DebrisItem {
    public Debris debris;
    public Vector2 location;

    public DebrisItem(Debris d, Vector2 l)
    {
        debris = d;
        location = l;
    }
}
