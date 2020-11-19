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
    // Card prefab
    public GameObject cardPrefab;

    // List of cards the player can use
    public List<Card> availableCards;

    private float centrePosition;

    public int cardHeight;
    public int cardPositionDelta;
    public int cardAngleDelta;

    private GameObject canvas;

    private List<GameObject> displayedCards;

    // Dialogue sgement to send card selection to
    private DialogueSegment listener;


    [Header("Card notification")]
    public AudioClip receiveSound;
    private AudioSource source;
    public GameObject notificationPrefab;
    [Min(0)] public float notificationDisplayTime;
    public Vector2 notificationPosition;

    void Awake()
    {
        listener = null;

        // Get canvas gameobject to display cards on
        canvas = GameObject.FindGameObjectWithTag("uiCanvas");

        centrePosition = Screen.width / 2;

        displayedCards = new List<GameObject>();
        source = gameObject.GetComponent<AudioSource>();
    }

    // Display cards on screen
    public void DisplayCards()
    {
        // Prevent displaying multiple times
        if (displayedCards.Count > 0)
        {
            return;
        }

        float position = centrePosition - cardPositionDelta * (availableCards.Count / 2);
        float angle = cardAngleDelta * (availableCards.Count / 2);

        // Create card objects
        foreach (var card in availableCards)
        {
            // Create and position card
            var newCard = Instantiate(cardPrefab, canvas.transform);
            newCard.transform.eulerAngles = new Vector3(0, 0, angle);
            newCard.transform.position = new Vector3(position, cardHeight - Mathf.Abs(angle * 1.8f), 0);
            position += cardPositionDelta;
            angle -= cardAngleDelta;

            // Set card data
            newCard.name = card.name;
            newCard.GetComponent<Image>().sprite = card.image;
            newCard.GetComponent<Button>().onClick.AddListener(delegate { SelectCard(card.name); } );

            // Add to list of displayed cards
            displayedCards.Add(newCard);
        }
    }

    // Destory all card display objects and report selected card
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

    // Give a new card to the player
    public void AddNewCard(Card card)
    {
        // Stop duplicate cards
        foreach (var c in availableCards)
        {
            if (c.name == card.name)
            {
                return;
            }
        }

        // Give feedback to player
        if (source && receiveSound)
        {
            source.PlayOneShot(receiveSound);
        }
        StartCoroutine(ShowNotification(card));

        availableCards.Add(card);
    }

    // Remove a specific card from the player
    public void RemoveCard(Card card)
    {
        availableCards.Remove(card);
    }

    // Remove a card with a specific name from the player
    public void RemoveCard(string cardName)
    {
        Card target = new Card();

        foreach (var card in availableCards)
        {
            if (card.name == cardName)
            {
                target = card;
                break;
            }
        }

        if (target.name != String.Empty)
        {
            availableCards.Remove(target);
        }
    }

    public void SetListener(DialogueSegment newListener)
    {
        listener = newListener;
    }

    IEnumerator ShowNotification(Card card)
    {
        GameObject notification;
        notification = Instantiate(notificationPrefab, canvas.transform);
        notification.transform.position = new Vector3(notificationPosition.x, notificationPosition.y, 0);
        notification.GetComponent<Image>().sprite = card.image;
        notification.transform.SetAsLastSibling();

        yield return new WaitForSeconds(notificationDisplayTime);

        // Destroy all children of card image
        foreach (Transform child in notification.transform)
        {
            Destroy(child.gameObject);
        }

        // Move card offscreen
        float bound = notification.GetComponent<RectTransform>().rect.height * -1;
        while (notification.transform.position.y > bound)
        {
            notification.transform.Translate(0, -600 * Time.deltaTime, 0);
            yield return null;
        }

        Destroy(notification);
    }
}
