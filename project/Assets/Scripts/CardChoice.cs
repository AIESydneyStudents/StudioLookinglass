using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardChoice : MonoBehaviour
{
    public GameObject cardPrefab;

    public List<Card> cards;

    public float height;
    public float deltaX;

    private List<GameObject> displayedCards;

    private float centre;

    private GameObject canvas;

    private PlayerCardManager playerManager;

    public bool returnContol = true;
    public bool allowInteraction = true;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("uiCanvas");
        centre = Screen.width / 2;

        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCardManager>();
    }

    // Display cards on screen
    public void DisplaySelection()
    {
        Vector2 position = new Vector2(centre, height);

        float offset = 0;

        displayedCards = new List<GameObject>();

        // Create card UI objects
        foreach (var card in cards)
        {
            // Create and position card
            var newCard = Instantiate(cardPrefab, canvas.transform);
            newCard.transform.position = new Vector3(position.x + offset, position.y, 0);
            offset += deltaX;
            if ((offset / deltaX) % 2 == 0)
            {
                offset -= deltaX;
                offset *= -1;
            }

            // Set card data
            newCard.name = card.name;
            newCard.GetComponent<Image>().sprite = card.image;
            newCard.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card.name); });

            // Add to list of displayed cards
            displayedCards.Add(newCard);
        }
    }

    public void SelectCard(string name)
    {
        // Clear UI
        foreach (var card in displayedCards)
        {
            Destroy(card);
        }
        displayedCards.Clear();

        // Add card to player's card manager
        foreach (var card in cards)
        {
            if (card.name == name)
            {
                playerManager.AddNewCard(card);
                break;
            }
        }

        // Return control to player
        if (returnContol)
        {
            playerManager.gameObject.GetComponent<PlayerMovement>().StartMovement();
        }

        // Allow interaction with object
        if (allowInteraction)
        {
            gameObject.GetComponent<NPCInteraction>().enabled = true;
        }
    }
}
