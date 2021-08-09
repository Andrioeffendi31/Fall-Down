using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDown : MonoBehaviour
{
    public Animator anim;
    public void slideDown()
    {
        anim.Play("slideDown");
    }
}
