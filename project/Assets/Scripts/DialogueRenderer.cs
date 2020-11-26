using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TextPosition
{
    Left,
    Right
}


[Serializable]
public class TextBox
{
    [TextArea] public string text;
    public string speaker;
    public TextPosition namePosition;
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

    static private readonly int main = 0;
    static private readonly int speaker = 1;

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
        TextBox t = new TextBox
        {
            text = text,
            speaker = string.Empty,
            boxSprite = null,
            leftSprite = null,
            rightSprite = null
        };
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
        newBox.name = "Textbox";
        newBox.transform.position = new Vector3(centre, boxSpawnHeight, 0);

        // Get text fields
        var childrenText = newBox.GetComponentsInChildren<Text>();

        // Add main text
        childrenText[main].text = box.text;

        // Add speaker text
        childrenText[speaker].text = box.speaker;

        // Detach position from parent
        Vector3 position = childrenText[speaker].transform.position - newBox.transform.position;
        if (box.namePosition == TextPosition.Right)
        {
            position.x *= -1;
            childrenText[speaker].transform.position = position + newBox.transform.position;
        }
        else
        {
            childrenText[speaker].transform.position = position + newBox.transform.position;
        }

        // Change image
        newBox.GetComponent<Image>().sprite = box.boxSprite;

        // Add to active list
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
