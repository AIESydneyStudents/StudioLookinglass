using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DialogueRenderer))]
public class DialogueSegment : MonoBehaviour
{
    public TextBox[] textBoxes;

    public KeyCode advanceKey = KeyCode.Return;

    private DialogueRenderer dialogueRenderer;

    private int position;

    // Start is called before the first frame update
    void Awake()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(advanceKey))
        {
            PushNext();
        }
    }

    private void OnEnable()
    {
        dialogueRenderer = GetComponent<DialogueRenderer>();
        position = 0;
        PushNext();
    }

    private void PushNext()
    {
        if (position < textBoxes.Length)
        {
            dialogueRenderer.ShowTextBox(ref textBoxes[position]);
            position++;
        }
        else
        {
            // Prompt player for cards
            this.enabled = false;
        }
    }
}
