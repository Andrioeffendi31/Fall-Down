using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSFX : MonoBehaviour
{
    public void PlaySFX()
    {
        GameObject.Find("BtnSound").GetComponent<AudioSource>().Play();
    }
}
