using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private GameObject player;

    public string segmentName;

    public float interactionDistance;

    public KeyCode interactionKey = KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is close enough and has pressed key
        if (Vector3.Distance(transform.position, player.transform.position) < interactionDistance && Input.GetKeyDown(interactionKey))
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
}
