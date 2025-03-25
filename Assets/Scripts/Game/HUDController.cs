using Health;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class HUDController: MonoBehaviour
	{
		private HealthController _playerHealth;
		private Image _healthBar;

		private void Start()
		{
			_playerHealth = GameObject.FindWithTag("Player").GetComponent<HealthController>();
			_healthBar = GameObject.Find("Health").GetComponent<Image>();
		}

		private void Update() { _healthBar.fillAmount = _playerHealth.Health / _playerHealth.MaxHealth; }
	}
}