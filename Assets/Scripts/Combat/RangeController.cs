using System;
using Prefab.Range;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Combat
{
	[Serializable] public class RangeController: CombatManager
	{
		[Header("Range")]
		public Transform origin;

		public float speed;
		public float dispersion;
		public bool surpass;

		public void Shoot(string typeOfBullet, Quaternion rotation)
		{
			if (!Enabled) return;

			GameObject prefab = Object.Instantiate(
				Resources.Load<GameObject>(PREFAB_ROUTE + typeOfBullet),
				origin.position,
				rotation * Quaternion.Euler(0, 0, Random.Range(-dispersion, dispersion))
			);

			prefab.TryGetComponent(out NormalBullet controller);
			controller.SetParameters(Character, damage * Multiplier, speed, surpass, prefabDuration);
			_ = Cooldown();
		}

		public void Shoot(string typeProjectile) { Shoot(typeProjectile, origin.rotation); }
	}
}