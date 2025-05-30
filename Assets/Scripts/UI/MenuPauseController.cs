using Character.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class MenuPauseController: MonoBehaviour
	{
		bool _isPaused;
		Text _text;
		PlayerController _player;

		void Awake()
		{
			_text = GameObject.FindWithTag("Pause").GetComponent<Text>();
			_player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape)) _isPaused = !_isPaused;

			if (_isPaused)
			{
				Time.timeScale = 0f;
				_text.text = "PAUSA";
				_player.Enabled = false;
			}
			else
			{
				Time.timeScale = 1f;
				_text.text = "";
				_player.Enabled = true;
			}
		}
	}
}