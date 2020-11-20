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

    public GameObject distantPrefab;
    private GameObject distant;

    protected override void Start()
    {
        base.Start();
        distant = Instantiate(distantPrefab, GameObject.FindGameObjectWithTag("uiCanvas").transform);
        if (gameObject.GetComponent<DialogueSegment>() == null && !forceOn)
        {
            this.enabled = false;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (distant)
        {
            distant.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PositionText();

        // Position distant image
        Vector3 textWorldPosition = transform.position + textOffset;
        distant.transform.position = cam.WorldToScreenPoint(textWorldPosition);

        // Check if player is close enough
        if (InteractionPossible())
        {
            ShowText();
            if (Input.GetKeyDown(interactionKey))
            {
                // Start segment
                StartSegment();
            }
            distant.SetActive(false);
        }
        else
        {
            HideText();
            distant.SetActive(true);
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
