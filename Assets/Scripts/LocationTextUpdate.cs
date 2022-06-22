using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LocationTextUpdate : MonoBehaviour
{
    public CountryData data;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = data.currentCountry;
    }
}
