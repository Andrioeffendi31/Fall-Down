using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
	private bool check = false;
	private int percentageReached = 0;

	public Slider slider;

	private void Update()
	{
		if (GameController.gameOver == false)
		{
			if (MusicController.songPosition + 1 >= MusicController.songLength)
			{
				percentageReached = 100;
			}
			else
			{
				percentageReached = (int)MusicController.songPercentage;
			}
			slider.value = percentageReached;
		}
		Debug.Log(percentageReached);
	}
}
