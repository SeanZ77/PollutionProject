using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CSVSpawnAmount : MonoBehaviour
{
    public TMP_InputField inputText;
    public int defaultSpawnAmount = 1000;
    static public int spawnAmount;

    void Awake()
    {
        spawnAmount = defaultSpawnAmount;
    }

    void Update()
    {
        if (inputText.text != "")
        {
            try
            {
                spawnAmount = int.Parse(inputText.text);
            }
            catch (System.ArgumentNullException)
            {
                spawnAmount = defaultSpawnAmount;
            }
        }
    }
}
