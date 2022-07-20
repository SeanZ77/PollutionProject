using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Debris In Scene")]
public class DebrisInScene : ScriptableObject
{
    public Dictionary<Debris, int> data = new Dictionary<Debris, int>();
}
