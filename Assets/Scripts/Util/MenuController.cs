using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
	public class MenuController: MonoBehaviour
	{
		public void StartLevel() { SceneManager.LoadScene("Level"); }

		public void QuitGame() { Application.Quit(); }
	}
}