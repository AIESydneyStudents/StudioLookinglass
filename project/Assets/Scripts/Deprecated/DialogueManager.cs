// Script is disabled DO NOT USE
/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct TextBox
{
    [TextArea] public string mainText;
    public Sprite image;
}

[DisallowMultipleComponent]
public class DialogueManager : MonoBehaviour
{
    public GameObject textboxPrefab;

    [Tooltip("Location on screen to spawn textboxes.")]
    public Vector2 spawnPosition;

    [Tooltip("Distance to move textboxes up.")]
    public float textboxMovement;

    public int maxActiveBoxes;

    // List of text scripts
    private List<TextDisplay> displays;

    // List of shown textboxes
    private List<GameObject> activeBoxes;

    private GameObject canvas;

    private bool isTextActive;

    // Start is called before the first frame update
    void Start()
    {
        activeBoxes = new List<GameObject>();
        isTextActive = false;

        // Get text scripts in object
        displays = new List<TextDisplay>();
        gameObject.GetComponents(displays);
        // Disable text displays
        foreach (var d in displays)
        {
            d.enabled = false;
        }

        // Get canvas
        canvas = GameObject.FindGameObjectWithTag("uiCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        // Testing function calls
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableText("Test");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableText("Test2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ClearBoxes();
        }
    }

    // Display a text box with a string
    public void CreateTextBox(string text)
    {
        TextBox temp = new TextBox();
        temp.mainText = text;
        temp.image = null;

        CreateTextBox(temp);
    }

    // Display a text box with a textbox struct
    public void CreateTextBox(TextBox box)
    {
        // Move existing boxes
        foreach (var b in activeBoxes)
        {
            b.transform.Translate(0, textboxMovement, 0);
        }

        // Create new box
        var newBox = Instantiate(textboxPrefab, canvas.transform);
        newBox.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        newBox.GetComponentInChildren<Text>().text = box.mainText;
        newBox.GetComponent<Image>().sprite = box.image;
        activeBoxes.Add(newBox);

        if (activeBoxes.Count > maxActiveBoxes)
        {
            Destroy(activeBoxes[0]);
            activeBoxes.RemoveAt(0);
        }
    }

    // Enable text display component with a given tag
    void EnableText(string tag)
    {
        if (isTextActive)
        {
            return;
        }

        foreach (var d in displays)
        {
            if (d.textTag == tag)
            {
                d.enabled = true;
                isTextActive = true;
                return;
            }
        }
    }

    // Called by display scripts when finished
    public void OnTextEnd()
    {
        isTextActive = false;
    }

    // Remove all active boxes from the screen
    public void ClearBoxes()
    {
        if (isTextActive)
        {
            return;
        }

        foreach (var box in activeBoxes)
        {
            Destroy(box);
        }

        activeBoxes.Clear();
    }
}
*/