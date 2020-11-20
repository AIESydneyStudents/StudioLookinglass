using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Param
{
    public Animator anim;
    public string parameter;
}

public class AnimatorToggle : MonoBehaviour
{
    public Param[] @params;

    public void StartTimer(float delay)
    {
        StartCoroutine(AnimTimer(delay));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    // Set parameters to true for a time then set to false
    private IEnumerator AnimTimer(float time)
    {
        // Set parameters to true
        foreach (var p in @params)
        {
            p.anim.SetBool(p.parameter, true);
        }

        yield return new WaitForSeconds(time);

        // Set parameters to false
        foreach (var p in @params)
        {
            p.anim.SetBool(p.parameter, false);
        }
    }
}