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
        counter.text = debrisInScene.getTotal().ToString();
    }
}
