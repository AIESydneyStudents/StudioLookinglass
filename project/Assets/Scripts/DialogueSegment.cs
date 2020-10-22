﻿using System;
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

[Serializable]
public struct CardResponse
{
    public string cardName;
    public string nextSegment;
}

[RequireComponent(typeof(DialogueRenderer))]
public class DialogueSegment : MonoBehaviour
{
    public string textTag;
    public TextBox[] textBoxes;

    public EndActions action;
    public string nextSegment;
    public CardResponse[] responses;

    private KeyCode advanceKey = KeyCode.Return;
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
        if (Input.GetKeyDown(advanceKey) && position <= textBoxes.Length)
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
            position++;
            OnTextEnd();
        }
    }

    // Action taken when text finishes
    void OnTextEnd()
    {
        switch (action)
        {
            case EndActions.ShowCards:
                ShowPlayerCards();
                break;
            case EndActions.StartSegment:
                StartNextSegment();
                break;
            case EndActions.End:
                dialogueRenderer.ClearAll();
                break;
            default:
                break;
        }
    }

    // Prompt player for cards
    void ShowPlayerCards()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var playerCards = player.GetComponent<PlayerCardManager>();
        playerCards.DisplayCards();
        playerCards.SetListener(this);
    }

    public void ReceiveCard(string card)
    {
        foreach (var r in responses)
        {
            if (r.cardName == card || r.cardName == String.Empty)
            {
                nextSegment = r.nextSegment;
                StartNextSegment();
                return;
            }
        }

        Debug.LogError("Unkown card \"" + card + "\" received.");
    }

    // Start a new segment with the nextSegment string
    void StartNextSegment()
    {
        var allSegments = gameObject.GetComponents<DialogueSegment>();
        foreach (var s in allSegments)
        {
            if (s.textTag == nextSegment && nextSegment != this.textTag)
            {
                s.enabled = true;
                this.enabled = false;
                return;
            }
        }

        Debug.LogError("Segment \"" + nextSegment + "\" not found.");
    }
}
