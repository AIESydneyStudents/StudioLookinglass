using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkMask : MonoBehaviour
{
    public string Parameter;
    public Animator MaskAnimator;

    private bool inTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    private void Update()
    {
        if(inTrigger == true)
            MaskAnimator.SetBool(Parameter, false);
        else
            MaskAnimator.SetBool(Parameter, true);
    }
}
