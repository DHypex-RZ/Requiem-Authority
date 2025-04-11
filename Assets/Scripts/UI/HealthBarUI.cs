using Character.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class HealthBarUI: MonoBehaviour
	{
		PlayerController _player;
		Image _healthBar;

		void Awake()
		{
			_player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
			_healthBar = transform.Find("HealthBar").GetComponent<Image>();
		}

		void Update() { _healthBar.fillAmount = _player.HealthController.CurrentHealth / _player.HealthController.Health; }
	}
}