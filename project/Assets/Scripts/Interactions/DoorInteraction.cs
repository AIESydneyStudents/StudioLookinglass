using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : InteractionBase
{   
    public string animatorParameter;

    private bool isOpen;
    private Animator animator;

    public AudioClip openSound;
    public AudioClip closeSound;
    private AudioSource audioSource;

    private bool canInteract;
    public float interactTime = 1;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        isOpen = false;
        canInteract = true;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PositionText();
        
        // Check if player is close enough
        if (InteractionPossible() && canInteract)
        {
            text.SetActive(true);
            if (Input.GetKeyDown(interactionKey))
            {
                isOpen = !isOpen;
                animator.SetBool(animatorParameter, isOpen);
                if (isOpen && openSound)
                {
                    audioSource.PlayOneShot(openSound);
                }
                else if (!isOpen && closeSound)
                {
                    audioSource.PlayOneShot(closeSound);
                }
                canInteract = false;
            }
        }
        else
        {
            text.SetActive(false);
        }

        if (!canInteract)
        {
            if (timer < interactTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                canInteract = true;
                timer = 0;
            }
        }
    }
}
