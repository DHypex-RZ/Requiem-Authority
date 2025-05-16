using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
	public class MenuController: MonoBehaviour
	{
		public void StartLevel() { SceneManager.LoadScene("Level"); }

		public void StartTutorial() { SceneManager.LoadScene("Tutorial"); }

		public void StartHardLevel() { SceneManager.LoadScene("Hardcore"); }


		public void QuitGame() { Application.Quit(); }
	}
}