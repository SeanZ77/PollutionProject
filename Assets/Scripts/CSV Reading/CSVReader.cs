using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : SpawnsMarkers
{
    public TextAsset csvFile;
    public int spawnAmount;
    public Debris[] types;
    public Debris placeholder;
    public Dictionary<string, Debris> debrisLookupFromDataName = new Dictionary<string, Debris>();
    public Dictionary<string, Debris> debrisLookupFromPresentationName = new Dictionary<string, Debris>();
    public Dictionary<string, List<GameObject>> allDebris = new Dictionary<string, List<GameObject>>();
    public ToggleChoice choices;
    public DebrisInScene debrisInScene;

    void Awake()
    {
        //"subscribing" to an event
        DebrisChoice.OnChoiceChanged += RefreshDebris;

        //adding debris names and types
        foreach (Debris d in types)
        {
            debrisLookupFromDataName.Add(d.dataName, d);
            debrisLookupFromPresentationName.Add(d.name, d);
            allDebris.Add(d.name, new List<GameObject>());
        }
        allDebris.Add(placeholder.name, new List<GameObject>());

        spawnAmount = CSVSpawnAmount.spawnAmount;

        List<Dictionary<string, object>> data = CSVReaderTest.Read(csvFile);
                
        for (int i = 0; i < spawnAmount; i++)
        {
            Debug.Log("Material: " + data[i]["material"].ToString());

            SpawnMarker(
                new Vector2(
                    float.Parse(data[i]["latitude"].ToString()), 
                    float.Parse(data[i]["longitude"].ToString())
                ), 
                getDebrisTypeFromDataName(data[i]["material"].ToString())
            );
        }


        foreach (Debris debris in types)
        {
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
            debrisInScene.data[getDebrisTypeFromPresentationName(objs.Key)] = 0;

            //print(objs.Key + " " + objs.Value.Count);
            foreach (GameObject o in objs.Value)
            {
                o.SetActive(false);
            }
        }
        List<GameObject> objects = new List<GameObject>();
        foreach (string name in choices.choice) {
            List<GameObject> o = allDebris[name];
            Debug.Log(o.Count);
            debrisInScene.data[getDebrisTypeFromPresentationName(name)] = o.Count;

            objects.AddRange(o);
        }
        foreach (GameObject o in objects) {
            o.SetActive(true);
        }
    }

    Debris getDebrisTypeFromDataName(string key) {
        Debris value;
        if (debrisLookupFromDataName.TryGetValue(key, out value))
        {
            return value;
        }
        else {
            print("Couldn't find the value for key: " + key);
            return placeholder;
        }
    }

    Debris getDebrisTypeFromPresentationName(string key)
    {
        Debris value;
        if (debrisLookupFromPresentationName.TryGetValue(key, out value))
        {
            return value;
        }
        else
        {
            print("Couldn't find the value for key: " + key);
            return placeholder;
        }
    }
}
