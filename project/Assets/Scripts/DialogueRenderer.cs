﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TextBox
{
    [TextArea] public string text;
    public Sprite boxSprite;
    public Sprite leftSprite;
    public bool flipLeft;
    public Sprite rightSprite;
    public bool flipRight;
    public AudioClip sound;
}

[DisallowMultipleComponent]
public class DialogueRenderer : MonoBehaviour
{
    public GameObject textboxPrefab;

    [Min(1)] public int maxActiveBoxes;

    public float boxSpawnHeight;
    public float textboxMovement;

    [Space]

    public GameObject portraitPrefab;

    private float centre;

    public Vector2 leftImageOffset;
    private Image leftImage;

    public Vector2 rightImageOffset;
    private Image rightImage;

    private GameObject canvas;
    private List<GameObject> activeBoxes;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        activeBoxes = new List<GameObject>();
        canvas = GameObject.FindGameObjectWithTag("uiCanvas");
        centre = Screen.width / 2;
        source = gameObject.GetComponent<AudioSource>();
    }

    // Show a default text box from a string
    public void ShowTextBox(string text)
    {
        TextBox t = new TextBox();
        t.text = text;
        t.boxSprite = null;
        t.leftSprite = null;
        t.rightSprite = null;
        ShowTextBox(t);
    }

    // Show a text box on the screen
    public void ShowTextBox(TextBox box)
    {
        // Create list if null
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

        // Run start if needed
        if (!canvas)
        {
            Start();
        }

        // Create new box
        var newBox = Instantiate(textboxPrefab, canvas.transform);
        newBox.transform.position = new Vector3(centre, boxSpawnHeight, 0);
        newBox.GetComponentInChildren<Text>().text = box.text;
        newBox.GetComponent<Image>().sprite = box.boxSprite;
        activeBoxes.Add(newBox);

        // Play sound
        if (source && box.sound)
        {
            source.PlayOneShot(box.sound);
        }

        // Set images
        if (box.leftSprite != null)
        {
            SetLeftImage(box.leftSprite, box.flipLeft);
        }
        if (box.rightSprite != null)
        {
            SetRightImage(box.rightSprite, box.flipRight);
        }

        // Remove old box past maximum
        if (activeBoxes.Count > maxActiveBoxes)
        {
            Destroy(activeBoxes[0]);
            activeBoxes.RemoveAt(0);
        }
    }

    public void ClearAll()
    {
        // Clear boxes
        foreach (var b in activeBoxes)
        {
            Destroy(b);
        }
        activeBoxes.Clear();
        
        // Clear portraits
        if (leftImage != null)
        {
            Destroy(leftImage.gameObject);
            leftImage = null;
        }
        if (rightImage != null)
        {
            Destroy(rightImage.gameObject);
            rightImage = null;
        }
    }

    void SetLeftImage(Sprite newSprite, bool flip)
    {
        // Create new image if does not exist
        if (leftImage == null)
        {
            leftImage = Instantiate(portraitPrefab, canvas.transform).GetComponent<Image>();
            leftImage.gameObject.transform.position = new Vector3(centre - leftImageOffset.x, leftImageOffset.y, 0);
        }

        // Change sprite
        leftImage.sprite = newSprite;

        // Horizontally mirror sprite
        if (flip)
        {
            leftImage.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            leftImage.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void SetRightImage(Sprite newSprite, bool flip)
    {
        // Create new image if does not exist
        if (rightImage == null)
        {
            rightImage = Instantiate(portraitPrefab, canvas.transform).GetComponent<Image>();
            rightImage.gameObject.transform.position = new Vector3(centre + rightImageOffset.x, rightImageOffset.y, 0);
        }

        // Change sprite
        rightImage.sprite = newSprite;

        // Horizontally mirror sprite
        if (flip)
        {
            rightImage.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rightImage.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
