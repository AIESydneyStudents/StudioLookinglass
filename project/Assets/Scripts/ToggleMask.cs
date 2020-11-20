using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMask : MonoBehaviour
{
    public GameObject Wall;

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
        if (inTrigger == true)
            Wall.layer = 10;
        else
            Wall.layer = 0;
    }
}
