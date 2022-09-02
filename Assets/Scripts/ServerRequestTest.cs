using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerRequestTest : MonoBehaviour
{
    public void GetPrediction(float latitude, float longitude)
    {
        StartCoroutine(SendGetRequest("https://oceanpollutionflask.bigphan.repl.co/" + latitude + "/" + longitude));
    }

    private static IEnumerator SendGetRequest(string url) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success) {
                print(webRequest.downloadHandler.text);
                RequestInfo ri = RequestInfo.createFromJson(webRequest.downloadHandler.text);
                print(ri.material);
                print(ri.quantity);
            }
        }
    }
}
