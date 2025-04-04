using Prefab;
using UnityEngine;

namespace Combat
{
	public class RangeController: CombatManager
	{
		[SerializeField] float speed;
		[SerializeField] float dispersion;
		[SerializeField] bool surpass;
		Transform _origin;
		BulletController _controller;

		protected override void Awake()
		{
			base.Awake();
			_origin = transform.Find("Projectile Origin");
		}

		public void Shoot(string typeProjectile, Quaternion rotation)
		{
			if (CheckCooldown) return;

			StartCoroutine(Cooldown());
			GameObject shoot = Instantiate(Resources.Load<GameObject>(PREFAB_ROUTE + typeProjectile), _origin.position, rotation * Quaternion.Euler(0, 0, Random.Range(-dispersion, dispersion)));
			if (shoot.TryGetComponent(out _controller)) _controller.SetParameters(character, damage * Multiplier, speed, surpass, duration);
		}

		public void Shoot(string typeProjectile) { Shoot(typeProjectile, _origin.rotation); }
	}
}