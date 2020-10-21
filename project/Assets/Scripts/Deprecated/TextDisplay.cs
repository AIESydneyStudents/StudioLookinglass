// Script is disabled DO NOT USE
/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DialogueManager))]
public class TextDisplay : MonoBehaviour
{
    public string textTag;

    public TextBox[] dialogue;

    private DialogueManager manager;

    private int position;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PushNext();
        }
    }

    private void OnEnable()
    {
        manager = gameObject.GetComponent<DialogueManager>();

        position = 0;

        PushNext();
    }

    void PushNext()
    {
        if (position < dialogue.Length)
        {
            manager.CreateTextBox(dialogue[position]);
            position++;
        }
        else
        {
            manager.OnTextEnd();
            this.enabled = false;
        }
    }
}
*/