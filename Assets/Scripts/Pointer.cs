using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject marker;
    public Transform map;
    public Vector2[] points;


    private void Start()
    {
        foreach (Vector2 p in points)
        {
            Vector2 pos = LongLat2XY(p.y, -p.x);
            Instantiate(marker, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            PlaceMarker();
        }
    }

    void PlaceMarker() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 realPos = Camera.main.ScreenToWorldPoint(mousePos);
        Instantiate(marker, realPos, Quaternion.identity);
    }

    Vector2 LongLat2XY(float longitude, float latitude) {
        float mapSize = map.localScale.x;
        float x = (longitude + 180) * (mapSize / 360);
        float latrad = latitude * Mathf.PI / 180;
        float mercn = Mathf.Log(Mathf.Tan((Mathf.PI / 4) + (latrad / 2)));
        float y = (mapSize / 2) - (mapSize * mercn / (2 * Mathf.PI));
        x -= mapSize / 2;
        y -= mapSize / 2;
        print(latitude + ", " + longitude + "==>" + x + ", " + y);
        return new Vector2(x, y);
    }
}
