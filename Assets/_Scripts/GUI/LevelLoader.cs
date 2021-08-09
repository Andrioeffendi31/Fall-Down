using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    private void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeValue()
    {
        progressText.text = slider.value * 100f + "%";
        Debug.Log(slider.value);
        if(slider.value * 100f == 100)
        {
            if (PlayerPrefs.GetInt("tutorial") != 0)
            {
                LoadLevel();
            }
            else
            {
                SceneManager.LoadScene("Tutorial");
            }
        }
    }

    //public void LoadLevel (int sceneIndex)
    //{
    //    StartCoroutine(LoadAsynchronously(sceneIndex));
    //}

    //IEnumerator LoadAsynchronously (int sceneIndex)
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

    //    loadingScreen.SetActive(true);

    //    while (!operation.isDone)
    //    {
    //        float progress = Mathf.Clamp01(operation.progress / .9f);
    //        Debug.Log(progress);

    //        slider.value = progress;
    //        progressText.text = progress * 100f + "%";

    //        yield return null;
    //    }
    //}
}
