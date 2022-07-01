using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolunteerMarkerInfo : MarkerInfo
{
    public string name;
    public string description;
    public string website;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI websiteText;

    void Start()
    {
        displayCanvas.SetActive(false);     
    }

    public override void OnMouseEnter()
    {
        base.OnMouseEnter();
        nameText.text = name;
        descriptionText.text = description;
        websiteText.text = website;
    }
}
