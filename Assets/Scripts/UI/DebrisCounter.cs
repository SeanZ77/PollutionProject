using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebrisCounter : MonoBehaviour
{
    public DebrisInScene debrisInScene;
    private TextMeshProUGUI counter;
    private int count = 0;

    void Awake()
    {
        counter = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        count = 0;
        foreach (KeyValuePair<Debris, int> keyValue in debrisInScene.data) {
            count += keyValue.Value;
        }
        counter.text = count.ToString();
    }
}
