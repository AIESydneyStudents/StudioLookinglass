﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    private GameObject player;

    public string segmentName;

    public float interactionDistance;

    public KeyCode interactionKey = KeyCode.E;

    [Space]

    public GameObject textPrefab;
    private Text text;
    public Vector2 textOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = Instantiate(textPrefab, GameObject.FindGameObjectWithTag("uiCanvas").transform).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        text.transform.Translate(new Vector3(textOffset.x, textOffset.y, 0));

        // Check if player is close enough
        if (Vector3.Distance(transform.position, player.transform.position) < interactionDistance)
        {
            text.enabled = true;
            if (Input.GetKeyDown(interactionKey))
            {
                // Start segment
                var allSegments = gameObject.GetComponents<DialogueSegment>();
                foreach (var segment in allSegments)
                {
                    if (segment.textTag == segmentName)
                    {
                        segment.enabled = true;
                    }
                }
            }
        }
        else
        {
            text.enabled = false;
        }
    }
}
