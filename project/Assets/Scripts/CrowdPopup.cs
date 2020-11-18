using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowdPopup : MonoBehaviour
{
    private GameObject player;
    public GameObject popupPrefab;
    private GameObject popup;

    public string text;
    public Sprite image;

    public float distance;
    public Vector3 offset;

    private Camera cam;

    [Min(0)] public float cooldown;
    private bool allowDisplay;
    private bool hasActivated;

    // Start is called before the first frame update
    void Awake()
    {
        // Get player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Setup popup object
        popup = Instantiate(popupPrefab, GameObject.FindGameObjectWithTag("uiCanvas").transform);
        popup.GetComponentInChildren<Text>().text = text;
        popup.GetComponent<Image>().sprite = image;

        cam = Camera.main;
        allowDisplay = true;
        hasActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            if (allowDisplay)
            {
                Position();
                popup.SetActive(true);
                hasActivated = true;
            }
        }
        else
        {
            if (allowDisplay && hasActivated)
            {
                StartCoroutine(Cooldown());
                allowDisplay = false;
            }
            popup.SetActive(false);
        }
    }

    private void Position()
    {
        Vector3 popupWorldPosition = transform.position + offset;
        popup.transform.position = cam.WorldToScreenPoint(popupWorldPosition);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        allowDisplay = true;
    }
}
