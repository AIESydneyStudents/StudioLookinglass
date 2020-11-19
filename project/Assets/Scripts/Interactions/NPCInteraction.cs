using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : InteractionBase
{
    public string segmentName;

    public bool lockPlayer = true;

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
                var allSegments = gameObject.GetComponents<DialogueSegment>();
                foreach (var segment in allSegments)
                {
                    if (segment.textTag == segmentName)
                    {
                        segment.enabled = true;
                        if (lockPlayer)
                        {
                            player.GetComponent<PlayerMovement>().StopMovement();
                            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        }
                        this.enabled = false;
                        break;
                    }
                }
            }
        }
        else
        {
            HideText();
        }
    }
}
