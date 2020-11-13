using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingAnimation : MonoBehaviour
{
    public float animSpeed;
    [Range(0, 1)] public float variation;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.speed = animSpeed;
        anim.Play(0, -1, 0);
    }

}
