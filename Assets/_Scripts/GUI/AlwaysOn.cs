using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysOn : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
