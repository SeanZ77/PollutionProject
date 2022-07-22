using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebrisAmount : MonoBehaviour
{
    public DebrisInScene debrisInScene;
    public TextMeshProUGUI debrisText;
    private int amount;

    void Update()
    {
        amount = 0;
        foreach (KeyValuePair<Debris, int> keyValue in debrisInScene.data) {
            amount += keyValue.Value;
        }
        debrisText.text = amount.ToString();
    }
}
