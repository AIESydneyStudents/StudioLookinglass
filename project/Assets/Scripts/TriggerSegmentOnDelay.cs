using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSegmentOnDelay : MonoBehaviour
{
    public string segment;
    [Min(0)] public float delayTime;
    private float elaspedTime;
    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (elaspedTime >= delayTime)
        {
            StartSegment();
            active = false;
            elaspedTime = 0;
        }
        else if (active)
        {
            elaspedTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        active = true;
        elaspedTime = 0;
    }

    public void StartTimer(float time)
    {
        delayTime = time;
        StartTimer();
    }

    private void StartSegment()
    {
        var allSegments = gameObject.GetComponents<DialogueSegment>();
        foreach (var s in allSegments)
        {
            if (s.textTag == segment)
            {
                s.enabled = true;
                this.enabled = false;
                return;
            }
        }

        Debug.LogError("Segment \"" + segment + "\" not found.");
    }
}
