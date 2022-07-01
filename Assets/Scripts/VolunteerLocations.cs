using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolunteerLocations : SpawnsMarkers
{
    public VolunteerLocation[] locations;

    void Start()
    {
        foreach (VolunteerLocation vl in locations) {
             GameObject m = SpawnMarker(vl.location);
             if (m.TryGetComponent(out VolunteerMarkerInfo mInfo))
             {
                mInfo.name = vl.name;
                mInfo.description = vl.description;
                mInfo.website = vl.website;
             }
        }
    }

}
