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
        //break down CSV file
        string[] allLines = csvFile.text.Split("\n"[0]);
        print(allLines.Length);
        List<string> lines = new List<string>();
        for (int i = 1; i < allLines.Length; i++)
        {
            lines.Add(allLines[i]);
        }

        //collect info from file
        foreach (string line in lines)
        {
            print(line);
            string[] data = line.Split(',');
            print(data[3]);
            print(data[4]);
            float latitude = float.Parse(data[3]);
            float longitude = float.Parse(data[4]);

            vols.Add(new VolunteerLocation(data[0], data[1], data[2], new Vector2(latitude, longitude)));
        }

        //based on info, make a marker and supply appropriate information
        foreach (VolunteerLocation vL in vols) {
            GameObject m = SpawnMarker(vL.location);
            markers.Add(m);
            if (m.TryGetComponent(out VolunteerMarkerInfo mInfo))
            {
                mInfo.name = vL.name;
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
