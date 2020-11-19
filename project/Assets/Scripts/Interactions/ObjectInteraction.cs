using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ObjectInteraction : InteractionBase
{
    public UnityEvent triggerEvent;

    // Update is called once per frame
    void Update()
    {
        PositionText();

        // Check if player is close enough
        if (InteractionPossible())
        {
            ShowText();
            if (Input.GetKeyDown(interactionKey))
            {
                triggerEvent.Invoke();
            }
        }
        else
        {
            HideText();
        }
    }
}
