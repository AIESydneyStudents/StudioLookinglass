﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimation : MonoBehaviour
{
    private Animator anim;
    void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.Play(0, -1, Random.value);
    }
}
