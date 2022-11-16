using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlasticDisplay : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI velocity, depth, shape;

    public void SetValues(string v, string d, string s) {
        velocity.text = "Velocity: " + v;
        depth.text = "Depth: " + d;
        shape.text = "Shape: " + s;
    }

    private void OnMouseEnter()
    {
        canvas.SetActive(true);
    }

    private void OnMouseExit()
    {
        canvas.SetActive(false); 
    }
}
