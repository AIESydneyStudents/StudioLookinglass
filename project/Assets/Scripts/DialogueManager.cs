using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class DialogueManager : MonoBehaviour
{
    // List of text scripts
    private List<TextDisplay> displays;

    // Start is called before the first frame update
    void Start()
    {
        // Get text scripts in object
        gameObject.GetComponents<TextDisplay>(displays);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
