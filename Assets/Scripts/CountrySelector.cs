using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountrySelector : MonoBehaviour
{
    public string countryName;
    public CountryData data;

    private void OnMouseDown()
    {
        print(countryName);
        data.currentCountry = countryName;
    }
}
