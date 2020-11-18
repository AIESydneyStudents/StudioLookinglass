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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        popup = Instantiate(popupPrefab, GameObject.FindGameObjectWithTag("uiCanvas").transform);
        popup.GetComponentInChildren<Text>().text = text;
        popup.GetComponent<Image>().sprite = image;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < distance)
        {
            Position();
            popup.SetActive(true);
        }
        else
        {
            popup.SetActive(false);
        }
    }

    private void Position()
    {
        Vector3 popupWorldPosition = transform.position + offset;
        popup.transform.position = cam.WorldToScreenPoint(popupWorldPosition);
    }
}
