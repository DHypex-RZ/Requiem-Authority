using Combat.Prefab;
using UnityEngine;

namespace Combat
{
	public class RangeCombatController: CombatController
	{
		[SerializeField] private float speed;
		[SerializeField] private bool canSurpassEnemies;
		[SerializeField] private float dispersion;

		private Transform _origin;

		private void Start()
		{
			prefab = Resources.Load<GameObject>(ROUTE + "Bullet");
			_origin = gameObject.transform.Find("ProjectileOrigin");
		}

		public void Shoot(Quaternion rotation)
		{
			if (CheckTimer()) return;

			GameObject bullet = Instantiate(prefab, _origin.position, rotation * Quaternion.Euler(0, 0, Random.Range(-dispersion, dispersion)));
			ProjectileController controller = bullet.GetComponent<ProjectileController>();
			controller.SetParameters(damage, lifetime, speed, canSurpassEnemies);
		}

		public void Shoot() { Shoot(_origin.rotation); }
	}
}