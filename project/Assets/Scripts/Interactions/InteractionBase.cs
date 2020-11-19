using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InteractionBase : MonoBehaviour
{
    protected GameObject player;

    public float interactionDistance;

    public KeyCode interactionKey = KeyCode.E;

    public GameObject textPrefab;
    protected GameObject text;
    public Vector3 textOffset;

    private Camera cam;

    protected virtual void Awake()
    {
        cam = Camera.main;
    }

    protected virtual void Start()
    {
        Setup();
    }

    protected virtual void OnDisable()
    {
        if (text != null)
        {
            HideText();
        }
    }

    // Get player and create text
    protected void Setup()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = Instantiate(textPrefab, GameObject.FindGameObjectWithTag("uiCanvas").transform);
    }

    // Position UI text on the canvas
    protected void PositionText()
    {
        Vector3 textWorldPosition = transform.position + textOffset;
        text.transform.position = cam.WorldToScreenPoint(textWorldPosition);
    }

    // Get if player is close enough to interact
    protected bool InteractionPossible()
    {
        return (Vector3.Distance(transform.position, player.transform.position) < interactionDistance);
    }

    protected void ShowText()
    {
        text.SetActive(true);
    }

    protected void HideText()
    {
        text.SetActive(false);
    }
}
