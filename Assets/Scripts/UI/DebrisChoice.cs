using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebrisChoice : MonoBehaviour
{
    public Debris debris;
    public ToggleChoice tc;
    public TextMeshProUGUI text;
    public TextMeshProUGUI count;

    public delegate void DebrisRefresh();
    public static event DebrisRefresh OnChoiceChanged;

    private void Start()
    {
        tc.choice.Add(debris.name);
    }

    public void AddDebris(bool selected) {
        if (selected)
        {
            tc.choice.Add(debris.name);
        }
        else {
            tc.choice.Remove(debris.name);
        }

        //as long as the event is attached to some method
        if (OnChoiceChanged != null)
            OnChoiceChanged();
    }
    
}
