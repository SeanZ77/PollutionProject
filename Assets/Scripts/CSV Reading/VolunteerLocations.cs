using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolunteerLocations : SpawnsMarkers
{
    public List<GameObject> markers = new List<GameObject>();
    public TextAsset csvFile;

    void Start()
    {
        List<Dictionary<string, object>> data = CSVReaderTest.Read(csvFile);
        //collect info from file
        for (var i = 0; i < data.Count; i++)
        {
            //print(data[i]["Name"].ToString());
            float latitude = float.Parse(data[i]["Latitude"].ToString());
            float longitude = float.Parse(data[i]["Longitude"].ToString());
            markers.Add(SpawnMarker(new Vector2(latitude, longitude), new Volunteer(data[i]["Name"].ToString(), data[i]["Description"].ToString(), data[i]["Website"].ToString())));
        }
    }

    public GameObject SpawnMarker(Vector2 l, Volunteer v)
    {
        GameObject m = Instantiate(marker, LongLat2XY(l.y, -l.x), Quaternion.identity);

        if (marker.TryGetComponent(out VolunteerMarkerInfo mInfo))
        {
            mInfo.latitudeText.text = l.x.ToString();
            mInfo.longitudeText.text = l.y.ToString();
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
