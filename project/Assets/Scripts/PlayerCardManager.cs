﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Card
{
    public string name;
    public Sprite image;
}

[DisallowMultipleComponent]
public class PlayerCardManager : MonoBehaviour
{
    // Card prefab
    public GameObject cardPrefab;

    [Space]

    // List of cards the player can use
    public List<Card> availableCards;

    private GameObject canvas;

    private List<GameObject> displayedCards;

    // Dialogue sgement to send card selection to
    private DialogueSegment listener;

    void Awake()
    {
        listener = null;

        // Get canvas gameobject to display cards on
        canvas = GameObject.FindGameObjectWithTag("uiCanvas");

        displayedCards = new List<GameObject>();
    }

    // Display cards on screen
    public void DisplayCards()
    {
        // Prevent displaying multiple times
        if (displayedCards.Count > 0)
        {
            return;
        }

        int offset = 300;
        int angle = 10;

        // Create card objects
        foreach (var card in availableCards)
        {
            // Create and position card
            var newCard = Instantiate(cardPrefab, canvas.transform);
            newCard.transform.position = new Vector3(offset, 100 - Mathf.Abs(angle), 0);
            newCard.transform.eulerAngles = new Vector3(0, 0, angle);
            offset += 150;
            angle -= 10;

            // Set card data
            newCard.name = card.name;
            newCard.GetComponentInChildren<Text>().text = card.name;
            newCard.GetComponent<Image>().sprite = card.image;
            newCard.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card.name); } );

            // Add to list of displayed cards
            displayedCards.Add(newCard);
        }
    }

    // Destory all card objects and report selected card
    public void SelectCard(string name)
    {
        // Destroy card objects
        foreach (var card in displayedCards)
        {
            Destroy(card);
        }
        displayedCards.Clear();

        if (listener != null)
        {
            listener.ReceiveCard(name);
            listener = null;
        }
    }

    public void AddNewCard(Card card)
    {
        if (!availableCards.Contains(card))
        {
            availableCards.Add(card);
        }
    }

    public void RemoveCard(Card card)
    {
        availableCards.Remove(card);
    }

    public void SetListener(DialogueSegment newListener)
    {
        listener = newListener;
    }
}
