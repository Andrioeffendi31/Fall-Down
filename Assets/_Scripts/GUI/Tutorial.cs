using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public void loadScene()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        SceneManager.LoadScene(0);
    }
}
