using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class Pointer : SpawnsMarkers
{
    public DebrisDictionary debrisDictionary;
    RequestInfo requestInfo;
    Debris AIDebris;
    public DebrisInScene debrisInScene;
    [SerializeField]

    void Awake()
    {
        foreach(var d in debrisDictionary.dictionary)
        {
            print(d.Value.dataName);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            StartCoroutine(PlaceMarker());
        }
    }

    public IEnumerator PlaceMarker()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 realPos = Camera.main.ScreenToWorldPoint(mousePos);
        realPos.z = 0;
        GameObject placedMarker = Instantiate(marker, realPos, Quaternion.identity);
        Vector2 realWorldPos = XY2LongLat(realPos.x, realPos.y);

        yield return StartCoroutine(SendGetRequest("https://oceanpollutionflask.bigphan.repl.co/" + realWorldPos.x + "/" + realWorldPos.y));
        AIDebris = debrisDictionary.FindDebrisFromDataName(requestInfo.material);
        print(AIDebris);

        if (placedMarker.TryGetComponent(out DebrisMarkerData mInfo)) {
            mInfo.name = AIDebris.name;
            mInfo.descriptionText.text = AIDebris.description;
            mInfo.img.sprite = AIDebris.image;
            mInfo.ChangeMarker(AIDebris.color, AIDebris.icon);
            mInfo.latitudeText.text = realWorldPos.x.ToString();
            mInfo.longitudeText.text = realWorldPos.y.ToString();
        }

        //debrisInScene.pointerData[choice.debris]++;
        int temp = 0;
        if (debrisInScene.pointerData.TryGetValue(AIDebris, out temp))
        {
            debrisInScene.pointerData[AIDebris]++;
        }
        else {
            debrisInScene.pointerData.Add(AIDebris, 1);
        }
    }

    private IEnumerator SendGetRequest(string url)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                requestInfo = RequestInfo.createFromJson(webRequest.downloadHandler.text);
                print(requestInfo.material);
                print(requestInfo.quantity);
            }
        }
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
}
