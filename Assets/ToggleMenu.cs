using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public GameObject toggle;
    public Debris[] debris;
    public ToggleChoice tc;

    private void Awake()
    {
        foreach (Debris d in debris) {
            MakeToggleObject(d);
        }
    }

    private void MakeToggleObject(Debris d) {
        GameObject o = Instantiate(toggle, transform);
        if (o.TryGetComponent(out DebrisChoice dC)) {
            dC.debris = d;
            dC.text.text = d.name;
            dC.tc = tc;
        }
    }
}
