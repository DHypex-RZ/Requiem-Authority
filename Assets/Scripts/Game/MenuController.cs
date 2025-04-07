using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
	public class MenuController: MonoBehaviour
	{
		public void StartLevel() { SceneManager.LoadScene("Level"); }

		public void QuitGame() { Application.Quit(); }
	}
}