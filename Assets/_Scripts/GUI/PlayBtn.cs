using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayBtn : MonoBehaviour
{
    public GameObject Hide;
    public GameObject Active, Active2;
    public GameObject next, prev, creditBtn;

    public Animator ballAnim;

    public void PlayGame()
    {
        ballAnim.SetBool("isDrop", true);
        GameObject.Find("PlayBtnSound").GetComponent<AudioSource>().Play();
        this.gameObject.GetComponent<Button>().enabled = false;
        next.gameObject.GetComponent<Button>().enabled = false;
        prev.gameObject.GetComponent<Button>().enabled = false;
        creditBtn.gameObject.GetComponent<Button>().enabled = false;
        StartCoroutine(WaitPlayScene());
    }

    public void enablePlayBtn()
    {
        this.gameObject.GetComponent<Button>().enabled = true;
        next.gameObject.GetComponent<Button>().enabled = true;
        prev.gameObject.GetComponent<Button>().enabled = true;
        creditBtn.gameObject.GetComponent<Button>().enabled = true;
    }

    IEnumerator WaitPlayScene()
    {
        yield return new WaitForSeconds(3);
        HideGameObject();
    }

    public void HideGameObject()
    {
        Hide.SetActive(false);
        Active.SetActive(true);
        Active2.SetActive(true);
    }
}
