using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject panel;
    private bool paused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused)
            {
                Time.timeScale = 0;
                panel.SetActive(true);
                paused = true;
            }
            else {
                Time.timeScale = 1;
                panel.SetActive(false);
                paused = false;
            }

        }
    }
}
