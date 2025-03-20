using System.Collections;
using Health;
using UnityEngine;

namespace Combat
{
	public class AbilityController: MonoBehaviour
	{
		[SerializeField] private float healAmount;
		[SerializeField] private float healCooldown;
		private bool _healInCooldown;
		[SerializeField] private float sniperDamage;
		[SerializeField] private float sniperSpeed;
		[SerializeField] private float sniperCooldown;
		private bool _sniperInCooldown;
		[SerializeField] private float shieldCooldown;
		private bool _shieldInCooldown;

		private HealthController _health;
		private RangeCombatController _sniper;

		private void Start()
		{
			_health = GetComponent<HealthController>();
			_sniper = GetComponent<RangeCombatController>();
		}

		public IEnumerator Heal()
		{
			if (_healInCooldown) yield break;

			_health.Heal(healAmount);
			_healInCooldown = true;

			yield return new WaitForSeconds(healCooldown);

			_healInCooldown = false;
		}

		public IEnumerator Sniper()
		{
			if (_sniperInCooldown) yield break;

			_sniper.SpecialShoot(sniperDamage, sniperSpeed);
			_sniperInCooldown = true;
			_sniper.enabled = false;

			yield return new WaitForSeconds(0.85f);

			_sniper.enabled = true;

			yield return new WaitForSeconds(sniperCooldown);

			_sniperInCooldown = false;
		}

		public IEnumerator Shield()
		{
			if (_shieldInCooldown) yield break;

			_shieldInCooldown = true;

			Instantiate(
				Resources.Load<GameObject>("Prefabs/Combat/Shield"),
				new Vector3(transform.position.x + 1, transform.position.y, transform.right.z),
				Quaternion.identity
			);

			yield return new WaitForSeconds(shieldCooldown);

			_shieldInCooldown = false;
		}
	}
}