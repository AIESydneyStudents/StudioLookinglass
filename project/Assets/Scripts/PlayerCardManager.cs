using System;
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
    // List of cards the player can use
    private List<Card> availableCards;

    // List of all cards
    public List<Card> allCards;

    [Space]

    // Card prefab
    public GameObject cardPrefab;

    private GameObject canvas;

    private List<GameObject> displayedCards;


    // Start is called before the first frame update
    void Start()
    {
        // Get canvas gameobject to display cards on
        canvas = GameObject.FindGameObjectWithTag("uiCanvas");

        availableCards = new List<Card>();

        // Display cards -TEST-

        foreach (var card in allCards)
        {
            availableCards.Add(card);
        }

        DisplayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Display cards on screen
    public void DisplayCards()
    {
        int offset = 0;

        // Create card objects
        foreach (var card in availableCards)
        {
            // Create and position card
            var newCard = Instantiate(cardPrefab, canvas.transform);
            newCard.transform.position = new Vector3(400 + offset, 150, 0);
            offset += 200;

            // Set card data
            newCard.name = card.name;
            newCard.GetComponentInChildren<Text>().text = card.name;
            newCard.GetComponent<Image>().sprite = card.image;
            // TODO: Add OnClick event
        }
    }

    // 
    public void SelectCard(string name)
    {
        // Destroy card objects
    }
}
