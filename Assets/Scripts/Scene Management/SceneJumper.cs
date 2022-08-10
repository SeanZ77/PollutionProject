using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneJumper : MonoBehaviour
{
    public Slider slider;
    public GameObject[] buttons;

    public void JumpScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void JumpSceneWithLoadScreen(string name) {
        StartCoroutine(JumpSceneAsync(name));
    }

    private IEnumerator JumpSceneAsync(string name) {
        slider.gameObject.SetActive(true);
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].SetActive(false);
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);
        while (!asyncOperation.isDone) {
            print("loading...");
            float progress = Mathf.Clamp01(asyncOperation.progress/0.9f);
            Debug.Log(asyncOperation.progress);
            slider.value = progress;
            yield return null;
        }
    }
}
