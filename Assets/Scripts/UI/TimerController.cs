using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class TimerController: MonoBehaviour
	{
		Text _text;
		float _timer;
		void Awake() { _text = GetComponent<Text>(); }

		void Update()
		{
			_timer += Time.deltaTime;
			int minutos = Mathf.FloorToInt(_timer / 60f);
			int segundos = Mathf.FloorToInt(_timer % 60f);
			int milisegundos = Mathf.FloorToInt(_timer * 1000f % 1000);
			_text.text = $"{minutos:00}:{segundos:00}:{milisegundos:000}";
		}
	}
}