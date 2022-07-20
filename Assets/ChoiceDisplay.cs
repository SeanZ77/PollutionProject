using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceDisplay : MonoBehaviour
{
    public PointerChoice choice;
    private Image image;


    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.sprite = choice.debris.icon;
        image.color = choice.debris.color;
    }
}
