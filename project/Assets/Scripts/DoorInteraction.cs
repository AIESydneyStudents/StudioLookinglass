using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour
{
    private GameObject player;

    public string animatorParameter;

    public float interactionDistance;

    public KeyCode interactionKey = KeyCode.E;

    [Space]

    public GameObject textPrefab;
    private Text text;
    public Vector3 textOffset;

    private bool isOpen;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = Instantiate(textPrefab, GameObject.FindGameObjectWithTag("uiCanvas").transform).GetComponent<Text>();
        animator = gameObject.GetComponent<Animator>();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 textWorldPosition = transform.position + textOffset;
        text.transform.position = Camera.main.WorldToScreenPoint(textWorldPosition);

        // Check if player is close enough
        if (Vector3.Distance(transform.position, player.transform.position) < interactionDistance)
        {
            text.enabled = true;
            if (Input.GetKeyDown(interactionKey))
            {
                isOpen = !isOpen;
                animator.SetBool(animatorParameter, isOpen);
            }
        }
        else
        {
            text.enabled = false;
        }
    }
}
