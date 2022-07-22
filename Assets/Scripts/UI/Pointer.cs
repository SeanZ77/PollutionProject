using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : SpawnsMarkers
{
    public Debris[] debrisTypes;
    public PointerChoice choice;
    public DebrisInScene debrisInScene;
    [SerializeField]
    private int choiceIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            PlaceMarker();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            print("choice up");
            choiceIndex++;
            if (choiceIndex > debrisTypes.Length - 1)
            {
                choiceIndex = 0;
            }
            choice.debris = debrisTypes[choiceIndex];
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            print("choice down");
            choiceIndex--;
            if (choiceIndex < 0)
            {
                choiceIndex = debrisTypes.Length - 1;
            }
            choice.debris = debrisTypes[choiceIndex];
        }

        
    }
      
    void PlaceMarker() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 realPos = Camera.main.ScreenToWorldPoint(mousePos);
        GameObject placedMarker = Instantiate(marker, realPos, Quaternion.identity);
        
        if (placedMarker.TryGetComponent(out DebrisMarkerData mInfo)) {
            mInfo.name = choice.debris.name;
            mInfo.description = choice.debris.description;
            mInfo.image = choice.debris.image;
            mInfo.ChangeMarker(choice.debris.color, choice.debris.icon);
        }

        debrisInScene.data[choice.debris]++;
    }

}
