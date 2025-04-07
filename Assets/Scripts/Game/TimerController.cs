using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class TimerController: MonoBehaviour
	{
		Text _text;
		float _timer;
		void Awake() { _text = GetComponent<Text>(); }

		void Update()
		{
			_timer += Time.deltaTime;
			_text.text = _timer.ToString("0 : 00");
		}
	}
}