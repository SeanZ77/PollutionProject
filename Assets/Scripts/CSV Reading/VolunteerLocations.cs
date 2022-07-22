using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolunteerLocations : SpawnsMarkers
{
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
            SpawnMarker(new Vector2(latitude, longitude), new Volunteer(data[0], data[1], data[2]));
        }
    }

    public GameObject SpawnMarker(Vector2 l, Volunteer v)
    {
        GameObject m = Instantiate(marker, LongLat2XY(l.y, -l.x), Quaternion.identity);

        if (marker.TryGetComponent(out VolunteerMarkerInfo mInfo))
        {
            mInfo.latitudeText.text = l.y.ToString();
            mInfo.longitudeText.text = l.x.ToString();
            mInfo.nameText.text = v.name;
            mInfo.descriptionText.text = v.description;
            mInfo.websiteText.text = v.website;
        }

        return m;
    }

    public void ToggleLocations(bool enabled) {
        foreach (GameObject marker in markers)
        {
            marker.SetActive(enabled);
        }
    }

}
