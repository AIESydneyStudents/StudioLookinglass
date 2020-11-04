using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardChoice : MonoBehaviour
{
    public GameObject cardPrefab;

    public List<Card> cards;

    public float height;
    public float xOffset;

    private List<GameObject> displayedCards;

    private float centre;

    private GameObject canvas;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("uiCanvas");
        centre = canvas.GetComponent<RectTransform>().rect.width / 2;
    }

    public void DisplaySelection()
    {
        Vector2 position = new Vector2(centre, height);

        // Create card UI objects
        foreach (var card in cards)
        {

        }
    }
}
