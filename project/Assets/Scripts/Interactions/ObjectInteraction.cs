using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ObjectInteraction : InteractionBase
{
    public UnityEvent triggerEvent;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        PositionText();

        // Check if player is close enough
        if (InteractionPossible())
        {
            text.SetActive(true);
            if (Input.GetKeyDown(interactionKey))
            {
                triggerEvent.Invoke();
            }
        }
        else
        {
            text.SetActive(false);
        }
    }
}
