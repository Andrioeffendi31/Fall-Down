﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUp : MonoBehaviour
{
    public Animator anim;
    public void slideUp()
    {
        anim.Play("slideUp");
    }
}
