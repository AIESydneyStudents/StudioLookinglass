using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : InteractionBase
{
    public string segmentName;

    public bool lockPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = Instantiate(textPrefab, GameObject.FindGameObjectWithTag("uiCanvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        PositionText();

        // Check if player is close enough
        if (InteractionPossible())
        {
            text.SetActive(true);
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
                            player.GetComponent<PlayerMovement>().allowMovement = false;
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
