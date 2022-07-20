using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolunteerLocations : SpawnsMarkers
{
    public List<VolunteerLocation> vols = new List<VolunteerLocation>();
    public List<GameObject> markers = new List<GameObject>();
    public TextAsset csvFile;

    void Start()
    {
        string[] allLines = csvFile.text.Split("\n"[0]);
        List<string> lines = new List<string>();
        for (int i = 0; i < lines.Count; i++)
        {
            lines.Add(allLines[i]);
        }
        foreach (string line in lines)
        {
            string[] data = line.Split(',');
            float latitude = float.Parse(data[3]);
            float longitude = float.Parse(data[4]);

            vols.Add(new VolunteerLocation(data[0], data[1], data[2], new Vector2(latitude, longitude)));
        }

        foreach (VolunteerLocation vL in vols) {
            GameObject m = SpawnMarker(vL.location);
            markers.Add(m);
            if (m.TryGetComponent(out VolunteerMarkerInfo mInfo))
            {
                mInfo.name = name;
                mInfo.description = vL.description;
                mInfo.website = vL.website;
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
