using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    [Space]
    public UnityEvent onOpen;
    public UnityEvent onClose;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
            ShowText();
            if (Input.GetKeyDown(interactionKey))
            {
                if (isOpen)
                {
                    CloseDoor();
                }
                else
                {
                    OpenDoor();
                }
            }
        }
        else
        {
            HideText();
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

    public void OpenDoor()
    {
        if (isOpen) { return; }

        isOpen = true;

        if (openSound)
        {
            audioSource.PlayOneShot(openSound);
        }

        onOpen.Invoke();

        DoorChangeCommon();
    }

    public void CloseDoor()
    {
        if (!isOpen) { return; }

        isOpen = false;

        if (closeSound)
        {
            audioSource.PlayOneShot(closeSound);
        }

        onClose.Invoke();

        DoorChangeCommon();
    }

    private void DoorChangeCommon()
    {
        animator.SetBool(animatorParameter, isOpen);
        canInteract = false;
    }
}
