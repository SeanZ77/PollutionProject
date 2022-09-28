using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsMarkers : MonoBehaviour
{
    public GameObject marker;
    public Transform map;

    public Vector2 LongLat2XY(float longitude, float latitude)
    {
        float mapSize = map.localScale.x;
        float x = (longitude + 180) * (mapSize / 360);
        float latrad = latitude * Mathf.PI / 180;
        float mercn = Mathf.Log(Mathf.Tan((Mathf.PI / 4) + (latrad / 2)));
        if (float.IsNaN(mercn)) {
            print(latitude);
        }
        float y = (mapSize / 2) - (mapSize * mercn / (2 * Mathf.PI));
        x -= mapSize / 2;
        y -= mapSize / 2;
        return new Vector2(x, y);
    }

    virtual public GameObject SpawnMarker(Vector2 l)
    {
        GameObject m = Instantiate(marker, LongLat2XY(l.y, -l.x), Quaternion.identity);

        if (m.TryGetComponent(out MarkerInfo mInfo))
        {
            mInfo.latitudeText.text = l.y.ToString();
            mInfo.longitudeText.text = l.x.ToString();
        }

        return m;
    }
}
