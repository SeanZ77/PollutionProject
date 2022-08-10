using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CSVSpawnAmount : MonoBehaviour
{
    public TMP_InputField inputText;
    public int spawnAmount = 1000;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        if (inputText.text != "")
        {
            spawnAmount = int.Parse(inputText.text);
        }
    }
}
