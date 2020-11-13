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

    protected void OnDisable()
    {
        if (text != null)
        {
            HideText();
        }
    }

    protected void Setup()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = Instantiate(textPrefab, GameObject.FindGameObjectWithTag("uiCanvas").transform);
    }

    protected void PositionText()
    {
        Vector3 textWorldPosition = transform.position + textOffset;
        text.transform.position = Camera.main.WorldToScreenPoint(textWorldPosition);
    }

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
