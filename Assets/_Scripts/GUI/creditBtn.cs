using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditBtn : MonoBehaviour
{
    public GameObject Hide;
    public GameObject Active;
    
    public void HideGameObject()
    {
        Hide.SetActive(false);
        Active.SetActive(true);
    }
   
}
