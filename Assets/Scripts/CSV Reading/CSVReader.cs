using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : SpawnsMarkers
{
    public TextAsset csvFile;
    public int spawnAmount;
    public Debris[] types;
    public Debris placeholder;
    public Dictionary<string, Debris> debrisLookup = new Dictionary<string, Debris>();
    public Dictionary<string, List<GameObject>> allDebris = new Dictionary<string, List<GameObject>>();
    public ToggleChoice choices;
    public DebrisInScene debrisInScene;

    void Awake()
    {
        //"subscribing" to an event
        DebrisChoice.OnChoiceChanged += RefreshDebris;

        foreach (Debris d in types) {
            debrisLookup.Add(d.dataName, d);
            print(d.dataName);
            allDebris.Add(d.name, new List<GameObject>());
        }

        allDebris.Add(placeholder.name, new List<GameObject>());
    }

    void Start()
    {
        string[] allLines = csvFile.text.Split("\n"[0]);
        List<string> lines = new List<string>();
        for (int i = 0; i < spawnAmount; i++) {
            int index = Random.Range(1, spawnAmount+1);
            lines.Add(allLines[index]);
        }

        foreach (string line in lines) {
            string[] data = line.Split(',');

            float latitude = float.Parse(data[9]);
            float longitude = float.Parse(data[10]);
            string material = data[2];

            SpawnMarker(new Vector2(latitude, longitude), getDebrisType(material));
        }

        foreach (Debris debris in types) {
            int length = allDebris[debris.name].Count;
            debrisInScene.data[debris] = length;
        }

        int amountOfPlaceholders = allDebris[placeholder.name].Count;
        debrisInScene.data[placeholder] = amountOfPlaceholders;
    }

    public GameObject SpawnMarker(Vector2 l, Debris d)
    {
        GameObject m = Instantiate(marker, LongLat2XY(l.y, -l.x), Quaternion.identity);

        if (m.TryGetComponent(out DebrisMarkerData mInfo))
        {
            mInfo.latitudeText.text = l.y.ToString();
            mInfo.longitudeText.text = l.x.ToString();
            mInfo.nameText.text = d.name;
            m.name = d.name;
            mInfo.descriptionText.text = d.description;
            mInfo.img.sprite = d.image;
            mInfo.ChangeMarker(d.color, d.icon);
            allDebris[d.name].Add(m);
        }

        return m;
    }

    private void RefreshDebris()
    {
        foreach (KeyValuePair<string, List<GameObject>> objs in allDebris)
        {
            print(objs.Key + " " + objs.Value.Count);
            foreach (GameObject o in objs.Value)
            {
                o.SetActive(false);
            }
        }
        List<GameObject> objects = new List<GameObject>();
        foreach (string name in choices.choice) {
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
            print("Couldn't find the value for key: " + key);
            return placeholder;
        }
    }
}
