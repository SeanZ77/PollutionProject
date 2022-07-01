using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "New Volunteer Location")]
public class VolunteerLocation : ScriptableObject
{
    public string name;
    public string description;
    public string website;
    public Vector2 location;

}
