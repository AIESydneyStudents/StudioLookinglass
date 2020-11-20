using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : InteractionBase
{
    public string segmentName;

    public bool lockPlayer = true;

    [Tooltip("Force interaction script on without segments")]
    public bool forceOn;

    protected override void Start()
    {
        base.Start();
        if (gameObject.GetComponent<DialogueSegment>() == null && !forceOn)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PositionText();

        // Check if player is close enough
        if (InteractionPossible())
        {
            ShowText();
            if (Input.GetKeyDown(interactionKey))
            {
                // Start segment
                StartSegment();
            }
        }
        else
        {
            HideText();
        }
    }

    // Start segment with default name
    public void StartSegment()
    {
        StartSegment(segmentName);
    }

    // Start segment with specific name
    public void StartSegment(string name)
    {
        var allSegments = gameObject.GetComponents<DialogueSegment>();
        foreach (var segment in allSegments)
        {
            if (segment.textTag == name)
            {
                segment.enabled = true;
                if (lockPlayer)
                {
                    player.GetComponent<PlayerMovement>().StopMovement();
                }
                this.enabled = false;
                break;
            }
        }
    }
}
