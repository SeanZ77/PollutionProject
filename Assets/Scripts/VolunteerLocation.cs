using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolunteerLocation
{
    public string name;
    public string description;
    public string website;
    public Vector2 location;

    public VolunteerLocation(string n, string d, string w, Vector2 l)
    {
        name = n;
        description = d;
        website = w;
        location = l;
    }
}
