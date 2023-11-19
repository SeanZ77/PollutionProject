using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversionTest : MonoBehaviour
{
    public Transform map;
    
    private void Start() {
        //latitude, longitude
        Vector2 l = new Vector2(16.76647f, 96.17346f);

        //longitude, -latitude
        Vector2 lToLongLat = LongLat2XY(l.y, -l.x);

        //-latitude, longitude
        Vector2 longLatToL = XY2LongLat(lToLongLat.x, lToLongLat.y);

        print(lToLongLat);
        print(longLatToL);
        Vector2 correctedLongLatToL = longLatToL * -Vector2.one;
        print(correctedLongLatToL);
    }

    public Vector2 LongLat2XY(float longitude, float latitude)
    {
        float mapSize = map.localScale.x;
        float x = (longitude + 180) * (mapSize / 360);
        float latrad = latitude * Mathf.PI / 180;
        print("LLXY latrad: " + latrad);
        float mercn = Mathf.Log(Mathf.Tan((Mathf.PI / 4) + (latrad / 2)));
        print("LLXY mercn: " + mercn);
        if (float.IsNaN(mercn)) {
            print(latitude);
        }
        float y = (mapSize / 2) - (mapSize * mercn / (2 * Mathf.PI));
        x -= mapSize / 2;
        y -= mapSize / 2;
        return new Vector2(x, y);
    }

    public Vector2 XY2LongLat(float x, float y)
    {
        float mapSize = map.localScale.x;
        x += mapSize/2;
        y += mapSize/2;

        float mercn = ((mapSize/2) - y) * (2 * Mathf.PI) / mapSize;
        print("XYLL mercn: " + mercn);
        float latrad = 2 * (Mathf.Atan(Mathf.Exp(mercn)) - (Mathf.PI / 4));
        print("XYLL latrad: " + latrad);
        float latitude = (latrad * 180) / Mathf.PI;

        float longitude = (x / (mapSize / 360)) - 180;

        return new Vector2(-latitude, longitude);
    }

    /*
    start with x,y

float mapSize = map.localScale.x;
x += mapSize/2
y += mapSize/2

y = (mapSize/2) - (mapSize * mercn / (2 * Mathf.PI))
(mapSize * mercn) / (2 * Mathf.PI) = (mapSize/2) - y
mapSize * mercn = ((mapSize/2) - y) * (2 * Mathf.PI)
mercn = ((mapSize/2) - y) * (2 * Mathf.PI) / mapSize

mercn = Mathf.Log(Mathf.Tan((Mathf.PI / 4) + (latrad / 2)))
((mapSize/2) - y) * (2 * Mathf.PI) / mapSize = Mathf.Log(Mathf.Tan((Mathf.PI / 4) + (latrad / 2)))
e^(((mapSize/2) - y) * (2 * Mathf.PI) / mapSize) = Mathf.Tan((Mathf.PI / 4) + (latrad / 2))
Mathf.aTan(e^(((mapSize/2) - y) * (2 * Mathf.PI) / mapSize)) = (Mathf.PI / 4) + (latrad / 2)
Mathf.aTan(e^(((mapSize/2) - y) * (2 * Mathf.PI) / mapSize)) - (Mathf.PI / 4) = (latrad / 2)
2(Mathf.aTan(e^(((mapSize/2) - y) * (2 * Mathf.PI) / mapSize)) - (Mathf.PI / 4)) = latrad

latrad = latitude * Mathf.PI / 180
2(Mathf.aTan(e^(((mapSize/2) - y) * (2 * Mathf.PI) / mapSize)) - (Mathf.PI / 4)) = latitude * Mathf.PI / 180
360(Mathf.aTan(e^(((mapSize/2) - y) * (2 * Mathf.PI) / mapSize)) - (Mathf.PI / 4)) = latitude * Mathf.PI
360(Mathf.aTan(e^(((mapSize/2) - y) * (2 * Mathf.PI) / mapSize)) / Mathf.PI = latitude

x = (longitude + 180) * (mapSize / 360)
x / (mapSize / 360) = longitude + 180
(x / (mapSize / 360)) - 180 = longitude
longitude = (x / (mapSize / 360)) - 180
    */
}
