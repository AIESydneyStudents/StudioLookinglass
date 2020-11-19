using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

    private KeyCode advanceKey = KeyCode.Space;
    private DialogueRenderer dialogueRenderer;
    private int position;

    public bool removeCard;
    public string[] cardsToTake;

    public bool giveCard;
    public Card cardToGive;

    public UnityEvent endEvent;
    public bool returnControl = true;
    public bool allowInteraction = true;

    private GameObject _player;
    private GameObject Player
    {
        get
        {
            if (_player)
            { }
            else
            {
                _player = GameObject.FindGameObjectWithTag("Player");
            }
            return _player;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(advanceKey) || Input.GetMouseButtonDown(0)) && position <= textBoxes.Length)
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
            dialogueRenderer.ShowTextBox(textBoxes[position]);
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
        // Invoke end event
        endEvent.Invoke();

        // Remove cards from the player
        if (removeCard)
        {
            foreach (var card in cardsToTake)
            {
                Player.GetComponent<PlayerCardManager>().RemoveCard(card);
            }
        }

        // Give cards to the player
        if (giveCard)
        {
            Player.GetComponent<PlayerCardManager>().AddNewCard(cardToGive);
        }

        // Select and take end action
        switch (action)
        {
            case EndActions.ShowCards:
                ShowPlayerCards();
                break;
            case EndActions.StartSegment:
                StartNextSegment();
                break;
            case EndActions.End:
                OnEnd();
                break;
            default:
                break;
        }
    }

    // Prompt player for cards
    void ShowPlayerCards()
    {
        var playerCards = Player.GetComponent<PlayerCardManager>();
        playerCards.DisplayCards();
        playerCards.SetListener(this);
    }

    public void ReceiveCard(string card)
    {
        foreach (var r in responses)
        {
            if (r.cardName == card)
            {
                nextSegment = r.nextSegment;
                break;
            }
            if (r.cardName == String.Empty)
            {
                nextSegment = r.nextSegment;
            }
        }
        StartNextSegment();
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

    void OnEnd()
    {
        // Clear text boxes
        dialogueRenderer.ClearAll();
        this.enabled = false;

        // Give player control
        if (returnControl)
        {
            Player.GetComponent<PlayerMovement>().StartMovement();
        }

        // Allow player to interact with the NPC again
        if (allowInteraction)
        {
            gameObject.GetComponent<NPCInteraction>().enabled = true;
        }
    }
}
