using Prefab;
using UnityEngine;

namespace Combat
{
	public class PhysicalController: CombatManager
	{
		[SerializeField] float force;
		AttackController _controller;

		public void Attack(string typeAttack)
		{
			if (CheckCooldown) return;

			StartCoroutine(Cooldown());
			GameObject attack = Instantiate(Resources.Load<GameObject>(PREFAB_ROUTE + typeAttack));
			if (attack.TryGetComponent(out _controller)) _controller.SetParameters(character, damage * Multiplier, force, duration);
		}
	}
}