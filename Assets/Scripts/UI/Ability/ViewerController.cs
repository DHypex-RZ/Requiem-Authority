using UnityEngine;
using UnityEngine.UI;

namespace UI.Ability
{
	public class ViewerController: MonoBehaviour
	{
		internal Character.Player.Ability Ability { get; set; }
		Image _background;
		Image _timer;
		Text _text;
		float _cooldown;

		void Awake()
		{
			_background = transform.Find("Background").GetComponent<Image>();
			_timer = transform.Find("Timer").GetComponent<Image>();
			_text = transform.Find("Timer").transform.Find("Text").GetComponent<Text>();
		}

		public void Start() { _text.text = Ability.assignedKey.ToString(); }

		void Update()
		{
			Color color;
			_timer.fillAmount = _cooldown / Ability.cooldown;

			if (Ability.Enabled)
			{
				_cooldown = 0;
				color = Color.white;
			}
			else
			{
				_cooldown += Time.deltaTime;
				color = Color.gray;
			}

			_background.color = color;
		}
	}
}