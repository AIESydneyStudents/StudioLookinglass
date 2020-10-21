using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct TextBox
{
    [TextArea] public string text;
    public Sprite boxSprite;
    public TextBox(string _text, Sprite _sprite)
    {
        text = _text;
        boxSprite = _sprite;
    }
}

[DisallowMultipleComponent]
public class DialogueRenderer : MonoBehaviour
{
    public GameObject textboxPrefab;

    public int maxActiveBoxes;

    public Vector2 spawnPosition;

    public float textboxMovement;

    private GameObject canvas;
    private List<GameObject> activeBoxes;

    // Start is called before the first frame update
    void Start()
    {
        activeBoxes = new List<GameObject>();
        canvas = GameObject.FindGameObjectWithTag("uiCanvas");
    }

    public void ShowTextBox(string text)
    {
        TextBox t = new TextBox(text, null);
        ShowTextBox(ref t);
    }

    public void ShowTextBox(ref TextBox box)
    {
        if (activeBoxes == null)
        {
            activeBoxes = new List<GameObject>();
        }

        // Move existing boxes
        foreach (var b in activeBoxes)
        {
            if (b != null)
            {
                b.transform.Translate(0, textboxMovement, 0);
            }
        }

        // Create new box
        var newBox = Instantiate(textboxPrefab, canvas.transform);
        newBox.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        newBox.GetComponentInChildren<Text>().text = box.text;
        newBox.GetComponent<Image>().sprite = box.boxSprite;
        activeBoxes.Add(newBox);

        // Remove old box past maximum
        if (activeBoxes.Count > maxActiveBoxes)
        {
            Destroy(activeBoxes[0]);
            activeBoxes.RemoveAt(0);
        }
    }

    public void ClearBoxes()
    {
        foreach (var b in activeBoxes)
        {
            Destroy(b);
        }
        activeBoxes.Clear();
    }
}
