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
    public Dictionary<string, List<GameObject>> allDebris = new Dictionary<string, List<GameObject>>();
    public ToggleChoice debris;
    public Debris placeholder;
    public DebrisInScene debrisInScene;


    void Awake()
    {
        //"subscribing" to an event
        DebrisChoice.OnChoiceChanged += RefreshDebris;

        foreach (Debris d in debris.types) {
            debrisLookup.Add(d.dataName, d);
            allDebris.Add(d.name, new List<GameObject>());
        }
    }

    void Start()
    {
        string[] allLines = csvFile.text.Split("\n"[0]);
        List<string> lines = new List<string>();
        for (int i = 0; i < spawnAmount; i++) {
            int index = Random.Range(1, spawnAmount+1);
            lines.Add(allLines[index]);
        }
        List<DebrisItem> points = new List<DebrisItem>();
        foreach (string line in lines) {
            string[] data = line.Split(',');
            float latitude = float.Parse(data[9]);
            float longitude = float.Parse(data[10]);
            positions.Add(new Vector2(latitude, longitude));
            locations.Add(data[11]);
            string material = data[2];
            materials.Add(material);
            info.Add(getDebrisType(material));
            points.Add(new DebrisItem(getDebrisType(material), new Vector2(latitude, longitude)));
        }

        foreach (DebrisItem p in points)
        {
            GameObject marker = SpawnMarker(p.location);
            if (marker.TryGetComponent(out DebrisMarkerData mInfo))
            {
                mInfo.name = p.debris.name;
                mInfo.description = p.debris.description;
                mInfo.image = p.debris.image;
                mInfo.ChangeMarker(p.debris.color, p.debris.icon);
                allDebris[p.debris.name].Add(marker);
            }
        }

        foreach (Debris debris in debris.types) {
            print(debris.name);
            int length = allDebris[debris.name].Count;
            debrisInScene.data[debris] = length;
        }

    }

    private void RefreshDebris()
    {
        print(debris.choice);
        foreach (KeyValuePair<string, List<GameObject>> objs in allDebris)
        {
            foreach (GameObject o in objs.Value)
            {
                o.SetActive(false);
            }
        }
        List<GameObject> objects = new List<GameObject>();
        foreach (string name in debris.choice) {
            List<GameObject> o = allDebris[name];
            objects.AddRange(o);
        }
        foreach (GameObject o in objects) {
            o.SetActive(true);
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
