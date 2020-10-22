using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTextFromButton : MonoBehaviour
{
    public KeyCode key;
    public string segment;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            var segments = gameObject.GetComponents<DialogueSegment>();
            foreach (var s in segments)
            {
                if (s.textTag == segment)
                {
                    s.enabled = true;
                    return;
                }
            }
        }
    }
}
