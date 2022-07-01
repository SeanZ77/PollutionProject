using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : SpawnsMarkers
{
    public DebrisItem[] points;


    private void Start()
    {
        foreach (DebrisItem p in points)
        {
            GameObject marker = SpawnMarker(p.location);
            if (marker.TryGetComponent(out DebrisMarkerData mInfo))
            {
                mInfo.name = p.debris.name;
                mInfo.description = p.debris.description;
                mInfo.image = p.debris.image;
            }
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

}
