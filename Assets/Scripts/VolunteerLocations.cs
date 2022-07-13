using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolunteerLocations : SpawnsMarkers
{
    public VolunteerLocation[] locations;
    public List<GameObject> markers = new List<GameObject>();

    void Start()
    {
        foreach (VolunteerLocation vl in locations) {
            GameObject m = SpawnMarker(vl.location);
            markers.Add(m);
            if (m.TryGetComponent(out VolunteerMarkerInfo mInfo))
            {
                mInfo.name = vl.name;
                mInfo.description = vl.description;
                mInfo.website = vl.website;
            }
        }
    }

    public void ToggleLocations(bool enabled) {
        foreach (GameObject marker in markers)
        {
            marker.SetActive(enabled);
        }
    }

}
