﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowdPopup : MonoBehaviour
{
    private GameObject player;
    public GameObject popupPrefab;
    private GameObject popup;

    public string text;
    public Sprite image;

    public float distance;
    public Vector3 offset;

    private Camera cam;

    [Min(0)] public float maxTime;
    [Min(0)] public float cooldown;
    private bool allowDisplay;
    private bool hasActivated;

    public bool followPlayer;

    private float displayTime;
    private bool isDisplaying;

    void Awake()
    {
        // Get player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Setup popup object
        popup = Instantiate(popupPrefab, GameObject.FindGameObjectWithTag("uiCanvas").transform);
        popup.GetComponentInChildren<Text>().text = text;
        popup.GetComponent<Image>().sprite = image;

        cam = Camera.main;
        allowDisplay = true;
        hasActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < distance && !AboveMaxTime())
        {
            if (allowDisplay)
            {
                Position();
                ShowPopup();
                hasActivated = true;
                isDisplaying = true;
            }
        }
        else
        {
            if ((allowDisplay && hasActivated) || AboveMaxTime())
            {
                StartCoroutine(Cooldown());
                allowDisplay = false;
                isDisplaying = false;
            }
            popup.SetActive(false);
        }

        // Increment time if displaying and a max time is set
        if (popup.activeSelf && isDisplaying && maxTime > 0)
        {
            displayTime += Time.deltaTime;
        }
    }

    // Position text on screen
    private void Position()
    {
        Vector3 popupWorldPosition = new Vector3();
        if (followPlayer)
        {
            popupWorldPosition = player.transform.position + offset;
        }
        else
        {
            popupWorldPosition = transform.position + offset;
        }
        popup.transform.position = cam.WorldToScreenPoint(popupWorldPosition);
    }

    IEnumerator Cooldown()
    {
        displayTime = 0;
        yield return new WaitForSeconds(cooldown);
        allowDisplay = true;
    }

    private void ShowPopup()
    {
        if (Time.timeScale > 0)
        {
            popup.SetActive(true);
        }
        else
        {
            popup.SetActive(false);
        }
    }

    private bool AboveMaxTime()
    {
        if (maxTime == 0)
        {
            return false;
        }
        else if (displayTime > maxTime)
        {
            return true;
        }
        return false;
    }
}
