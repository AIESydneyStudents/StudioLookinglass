using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class DialogueManager : MonoBehaviour
{
    public GameObject textboxPrefab;

    public Vector2 spawnPosition;

    public float textboxMovement;

    public int maxActiveBoxes;

    // List of text scripts
    private List<TextDisplay> displays;

    // List of shown textboxes
    private List<GameObject> activeBoxes;

    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        activeBoxes = new List<GameObject>();

        // Get text scripts in object
        displays = new List<TextDisplay>();
        gameObject.GetComponents<TextDisplay>(displays);

        // Get canvas
        canvas = GameObject.FindGameObjectWithTag("uiCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CreateTextBox("test " + Time.time.ToString());
        }
    }

    void CreateTextBox(string text)
    {
        // Move existing boxes
        foreach (var box in activeBoxes)
        {
            box.transform.Translate(0, textboxMovement, 0);
        }

        // Create new box
        var newBox = Instantiate(textboxPrefab, canvas.transform);
        newBox.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        newBox.GetComponentInChildren<Text>().text = text;
        activeBoxes.Add(newBox);

        if (activeBoxes.Count > maxActiveBoxes)
        {
            Destroy(activeBoxes[0]);
            activeBoxes.RemoveAt(0);
        }
    }
}
