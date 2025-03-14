using Health;
using UnityEngine;
using UnityEngine.UI;

namespace Scene
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

		void Update()
		{
			_healthBar.fillAmount = _playerHealth.Health / _playerHealth.MaxHealth;
		}
	}
}