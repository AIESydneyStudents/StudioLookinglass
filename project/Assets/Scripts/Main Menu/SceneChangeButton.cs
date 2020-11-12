using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        if (scene != string.Empty && SceneManager.GetActiveScene().name != scene)
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }
}
