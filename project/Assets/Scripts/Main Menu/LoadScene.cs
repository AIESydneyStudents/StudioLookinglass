using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public string defaultScene;

    public Text loadText;

    public void StartLoad()
    {
        StartLoad(defaultScene);
    }

    public void StartLoad(string scene)
    {
        StartCoroutine(Load(scene));
    }

    private IEnumerator Load(string name)
    {
        // Start loading scene
        var operation = SceneManager.LoadSceneAsync(name);
        // Enable text
        loadText.gameObject.SetActive(true);
        // Reset timescale
        Time.timeScale = 1;

        while (!operation.isDone)
        {
            // Get progress and make count to 100%
            float progress = operation.progress / 9 * 1000;
            loadText.text = "Loading... " + progress.ToString("N1") + "%";
            yield return null;
        }
    }
}
