using System;
using Prefab.Physical;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Combat
{
	[Serializable] public class PhysicalController: CombatManager
	{
		[Header("Physical")]
		public float pushForce;

		public void Attack(string typeOfAttack)
		{
			if (!Enabled) return;

			GameObject prefab = Object.Instantiate(Resources.Load<GameObject>(PREFAB_ROUTE + typeOfAttack));
			prefab.TryGetComponent(out NormalAttack controller);
			controller.SetParameters(Character, damage * Multiplier, pushForce, prefabDuration);
			_ = Cooldown();
		}
	}
}