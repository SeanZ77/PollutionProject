using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarkerInfo : MonoBehaviour
{
    public float longitude;
    public float latitude;
    public int shrinkThreshold = 3;
    public TextMeshProUGUI lat_text;
    public TextMeshProUGUI long_text;
    public GameObject displayCanvas;
    public Vector3 defaultScale;

    protected virtual void Awake()
    {
        defaultScale = transform.localScale;
    }

    private void Update()
    {
        if (Camera.main.orthographicSize < shrinkThreshold)
        {
            transform.localScale = defaultScale * Camera.main.orthographicSize / shrinkThreshold;
        }
        else
            transform.localScale = defaultScale;
    }

    public virtual void OnMouseEnter()
    {
        displayCanvas.SetActive(true);
        lat_text.text = latitude.ToString();
        long_text.text = longitude.ToString();
    }

    public void OnMouseExit()
    {
        displayCanvas.SetActive(false);
    }
}
