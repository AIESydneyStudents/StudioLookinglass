using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayedSceneChange : MonoBehaviour
{
    public string scene;
    public float time;

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(scene);
    }
}
