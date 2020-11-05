using System.Collections;
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
    public Vector3 textOffset;

    public bool lockPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = Instantiate(textPrefab, GameObject.FindGameObjectWithTag("uiCanvas").transform).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 textWorldPosition = transform.position + textOffset;
        text.transform.position = Camera.main.WorldToScreenPoint(textWorldPosition);

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
                        if (lockPlayer)
                        {
                            player.GetComponent<PlayerMovement>().enabled = false;
                        }
                        this.enabled = false;
                        break;
                    }
                }
            }
        }
        else
        {
            text.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (text != null)
        {
            text.enabled = false;
        }
    }
}
