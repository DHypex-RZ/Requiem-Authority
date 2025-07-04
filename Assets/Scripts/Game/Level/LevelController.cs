﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Level
{
	public abstract class LevelController: MonoBehaviour
	{
		protected static IEnumerator Menu()
		{
			yield return new WaitForSeconds(2f);

			SceneManager.LoadScene("Menu");
		}

		public abstract void LoseCondition();
	}
}