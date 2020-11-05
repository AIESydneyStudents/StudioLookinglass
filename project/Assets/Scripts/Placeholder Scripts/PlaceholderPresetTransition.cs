using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderPresetTransition : MonoBehaviour
{

    public GameObject PresetA;
    public GameObject PresetB;

    void OnTriggerEnter()
    {
        PresetA.SetActive(false);
        PresetB.SetActive(true);
        gameObject.SetActive(false);
    }
}
