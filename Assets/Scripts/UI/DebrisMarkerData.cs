using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebrisMarkerData : MarkerInfo
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI quantityText;
    public Image img;
    public SpriteRenderer spriteRenderer;

    //public override void OnMouseEnter()
    //{
    //    base.OnMouseEnter();
    //}

    public override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    public void ChangeMarker(Color color, Sprite sprite) {
        spriteRenderer.color = color;
        spriteRenderer.sprite = sprite;
    }
}
