using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ViewerController: MonoBehaviour
	{
		internal Character.Player.Ability Ability { get; set; }
		Image _background;
		Image _timer;
		float _cooldown;

		void Awake()
		{
			_background = transform.Find("Background").GetComponent<Image>();
			_timer = transform.Find("Timer").GetComponent<Image>();
		}

		public void Start() { _background.sprite = Ability.icon; }

		void Update()
		{
			_timer.fillAmount = _cooldown / Ability.cooldown;

			if (Ability.Enabled) _cooldown = 0;
			else _cooldown += Time.deltaTime;
		}
	}
}