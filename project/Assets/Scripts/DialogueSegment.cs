using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EndActions
{
    ShowCards,
    StartSegment,
    End
}

[RequireComponent(typeof(DialogueRenderer))]
public class DialogueSegment : MonoBehaviour
{
    public string textTag;
    public TextBox[] textBoxes;

    public KeyCode advanceKey = KeyCode.Return;
    public EndActions action;

    public string nextSegment;

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
            // Advance to next box
            dialogueRenderer.ShowTextBox(ref textBoxes[position]);
            position++;
        }
        else
        {
            OnTextEnd();
        }
    }

    // Action taken when text finishes
    void OnTextEnd()
    {
        switch (action)
        {
            case EndActions.ShowCards:
                GetPlayerCard();
                break;

            case EndActions.StartSegment:
                StartNextSegment();
                break;

            case EndActions.End:
                dialogueRenderer.ClearBoxes();
                break;

            default:
                break;
        }
    }

    // Prompt player for cards
    void GetPlayerCard()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerCards = player.GetComponent<PlayerCardManager>();
        playerCards.DisplayCards();
    }

    // Start a new segment with the nextSegment string
    void StartNextSegment()
    {
        var allSegments = gameObject.GetComponents<DialogueSegment>();
        foreach (var s in allSegments)
        {
            if (s.textTag == nextSegment)
            {
                s.enabled = true;
                this.enabled = false;
                return;
            }
        }
        // Log if the next segment is not found
        Debug.LogWarning("Next segment " + nextSegment + " not found");
    }
}
