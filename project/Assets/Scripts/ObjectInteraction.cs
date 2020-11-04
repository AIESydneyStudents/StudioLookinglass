using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ObjectInteraction : MonoBehaviour
{
    private GameObject player;

    public UnityEvent triggerEvent;

    public float interactionDistance;

    public KeyCode interactionKey = KeyCode.E;

    [Space]

    public GameObject textPrefab;
    private Text text;
    public Vector3 textOffset;

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
                triggerEvent.Invoke();
            }
        }
        else
        {
            text.enabled = false;
        }
    }
}
