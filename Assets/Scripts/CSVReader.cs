using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : SpawnsMarkers
{
    public int spawnAmount;
    public TextAsset csvFile;
    public List<Vector2> positions = new List<Vector2>();
    public List<string> locations = new List<string>();
    public List<string> materials = new List<string>();
    public List<Debris> info = new List<Debris>();
    public Dictionary<string, Debris> debrisLookup = new Dictionary<string, Debris>();

    public Debris placeholder;
    public Debris boatParts;
    public Debris plastic;
    public Debris microPlastics;
    public Debris fishingGear;
    public Debris glass;
    public Debris cloth;
    public Debris rubber;
    public Debris metal;
    
    void Awake()
    {
        debrisLookup.Add("BOAT PARTS", boatParts);
        debrisLookup.Add("PLASTIC", plastic);
        debrisLookup.Add("MICROPLASTICS", microPlastics);
        debrisLookup.Add("FISHING GEAR", fishingGear);
        debrisLookup.Add("GLASS", glass);
        debrisLookup.Add("CLOTH", cloth);
        debrisLookup.Add("RUBBER", rubber);
        debrisLookup.Add("METAL", metal);
    }

    void Start()
    {
        string[] allLines = csvFile.text.Split("\n"[0]);
        print(allLines.Length);
        List<string> lines = new List<string>();
        for (int i = 1; i < spawnAmount+1; i++) {
            print(allLines[i]);
            lines.Add(allLines[i]);
        }
        List<DebrisItem> points = new List<DebrisItem>();
        foreach (string line in lines) {
            string[] data = line.Split(',');
            float latitude = float.Parse(data[9]);
            float longitude = float.Parse(data[10]);
            positions.Add(new Vector2(latitude, longitude));
            locations.Add(data[11]);
            materials.Add(data[2]);
            info.Add(getDebrisType(data[2]));
            points.Add(new DebrisItem(getDebrisType(data[2]), new Vector2(latitude, longitude)));
        }

        foreach (DebrisItem p in points)
        {
            GameObject marker = SpawnMarker(p.location);
            if (marker.TryGetComponent(out DebrisMarkerData mInfo))
            {
                mInfo.name = p.debris.name;
                mInfo.description = p.debris.description;
                mInfo.image = p.debris.image;
                mInfo.ChangeMarker(p.debris.color);
            }
        }
    }

    Debris getDebrisType(string key) {
        Debris value;
        if (debrisLookup.TryGetValue(key, out value))
        {
            return value;
        }
        else {
            return placeholder;
        }
    }
}
