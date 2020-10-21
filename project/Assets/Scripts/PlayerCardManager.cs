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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
