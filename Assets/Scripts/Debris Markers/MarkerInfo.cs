using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarkerInfo : MonoBehaviour
{
    public int shrinkThreshold = 3;
    public TextMeshProUGUI latitudeText;
    public TextMeshProUGUI longitudeText;
    public GameObject displayCanvas;
    public Vector3 defaultScale;
    private bool canvasActive;

    protected virtual void Awake()
    {
        defaultScale = transform.localScale;
    }

    void Start()
    {
        displayCanvas.SetActive(false);     
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

    //public virtual void OnMouseEnter()
    //{
    //    displayCanvas.SetActive(true);
    //}

    public virtual void OnMouseDown()
    {
        canvasActive = !canvasActive;
        displayCanvas.SetActive(canvasActive);
    }

    //public void OnMouseExit()
    //{
    //    displayCanvas.SetActive(false);
    //}
}
