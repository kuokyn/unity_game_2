using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSettings : MonoBehaviour
{
    public void ButtonClick(string mode)
    {
        if (mode == "easy")
        {
			ChooseDifficulty.difficulty = ChooseDifficulty.Difficulties.EASY;
		}
		if(mode == "medium")
		{
			ChooseDifficulty.difficulty = ChooseDifficulty.Difficulties.MEDIUM;
		}
		if (mode == "hard")
		{
			ChooseDifficulty.difficulty = ChooseDifficulty.Difficulties.HARD;
		}
		if (mode == "expert")
		{
			ChooseDifficulty.difficulty = ChooseDifficulty.Difficulties.EXPERT;
		}
		SceneManager.LoadScene("Game");
	}

	public void Replay()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("StartMenu");
	}

	public void Quit()
	{
		Application.Quit();
	}
}
