using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour 
{
    public SongSelector songSelector;

    public int record;

    public TextMeshPro totalScoreText;
    public TextMeshPro recordText;
    public ScoreController scoreController;

    private void Start()
    {
        record = PlayerPrefs.GetInt("HighScore " + songSelector.songIndex);
        GameObject songSelectorObject = GameObject.FindGameObjectWithTag("SongSelector");

        if (songSelectorObject != null)
        {
            songSelector = songSelectorObject.GetComponent<SongSelector>();
        }
    }

    private void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("HighScore " + songSelector.songIndex));
        if (GameController.gameOver == true || GameController.win == true)
        {
            totalScoreText.text = scoreController.scoreValue + "p";

            if (scoreController.scoreValue > record)
            {
                record = scoreController.scoreValue;
                PlayerPrefs.SetInt("HighScore " + songSelector.songIndex, record);
            }
            else
            {
                record = PlayerPrefs.GetInt("HighScore " + songSelector.songIndex);
            }
            recordText.text = PlayerPrefs.GetInt("HighScore " + songSelector.songIndex) + "p";
        }
    }

}
