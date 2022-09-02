using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestInfo
{
    public string material;
    public int quantity;

    public static RequestInfo createFromJson(string jsonString) {
        return JsonUtility.FromJson<RequestInfo>(jsonString);
    }
}
