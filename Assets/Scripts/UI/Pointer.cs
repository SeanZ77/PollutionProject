using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            PlaceMarker();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            choiceIndex++;
            if (choiceIndex > debrisTypes.Length - 1)
            {
                choiceIndex = 0;
            }
            choice.debris = debrisTypes[choiceIndex];
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
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
        Vector3 realPos = Camera.main.ScreenToWorldPoint(mousePos);
        realPos.z = 0;
        GameObject placedMarker = Instantiate(marker, realPos, Quaternion.identity);
        
        if (placedMarker.TryGetComponent(out DebrisMarkerData mInfo)) {
            mInfo.name = choice.debris.name;
            mInfo.descriptionText.text = choice.debris.description;
            mInfo.img.sprite = choice.debris.image;
            mInfo.ChangeMarker(choice.debris.color, choice.debris.icon);
        }

        //debrisInScene.pointerData[choice.debris]++;
        int temp = 0;
        if (debrisInScene.pointerData.TryGetValue(choice.debris, out temp))
        {
            debrisInScene.pointerData[choice.debris]++;
        }
        else {
            debrisInScene.pointerData.Add(choice.debris, 1);
        }
    }

}
