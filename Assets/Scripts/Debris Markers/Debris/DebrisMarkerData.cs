using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebrisMarkerData : MarkerInfo
{
    public string name;
    public string description;
    public Sprite image;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image img;
    private SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        displayCanvas.SetActive(false);
    }

    public override void OnMouseEnter()
    {
        base.OnMouseEnter();
        nameText.text = name;
        descriptionText.text = description;
        img.sprite = image;
    }

    public void ChangeMarker(Color color, Sprite sprite) {
        spriteRenderer.color = color;
        spriteRenderer.sprite = sprite;
    }
}
