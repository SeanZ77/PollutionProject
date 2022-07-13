using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : SpawnsMarkers
{
    public Debris[] debrisTypes;
    private int choiceIndex = 0;

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
        GameObject placedMarker = Instantiate(marker, realPos, Quaternion.identity);

        Debris debris = debrisTypes[choiceIndex];
        if (placedMarker.TryGetComponent(out DebrisMarkerData mInfo)) {
            mInfo.name = debris.name;
            mInfo.description = debris.description;
            mInfo.image = debris.image;
            mInfo.ChangeMarker(debris.color, debris.icon);
        }
    }

}
