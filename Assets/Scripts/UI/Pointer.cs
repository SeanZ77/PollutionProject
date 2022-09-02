using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

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
            IncrementDebris();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DecrementDebris();
        }
    }

    public void IncrementDebris() {
        choiceIndex++;
        if (choiceIndex > debrisTypes.Length - 1)
        {
            choiceIndex = 0;
        }
        choice.debris = debrisTypes[choiceIndex];
    }

    public void DecrementDebris() {
        choiceIndex--;
        if (choiceIndex < 0)
        {
            choiceIndex = debrisTypes.Length - 1;
        }
        choice.debris = debrisTypes[choiceIndex];
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

    public void GetPrediction(float latitude, float longitude)
    {
        StartCoroutine(SendGetRequest("https://oceanpollutionflask.bigphan.repl.co/" + latitude + "/" + longitude));
    }

    private static IEnumerator SendGetRequest(string url)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                print(webRequest.downloadHandler.text);
                RequestInfo ri = RequestInfo.createFromJson(webRequest.downloadHandler.text);
                print(ri.material);
                print(ri.quantity);
            }
        }
    }

}
