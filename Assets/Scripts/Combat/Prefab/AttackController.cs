using UnityEngine;

namespace Combat.Prefab
{
	public class AttackController: PrefabController
	{
		public GameObject Parent { get; private set; }

		public void SetParameters(float damage, float lifetime, GameObject parent)
		{
			base.SetParameters(damage, lifetime);
			Parent = parent;
		}

		private void Start()
		{
			int position = Parent.transform.rotation.y == 0 ? 1 : -1;
			transform.position = Parent.transform.position + new Vector3(position, 0, 0);
			Destroy(gameObject, Lifetime);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.CompareTag("Projectile")) return;

			ProjectileController bullet = other.GetComponent<ProjectileController>();
			bullet.CanSurpassEnemies = false;
			bullet.GetComponent<SpriteRenderer>().flipX = true;
			bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * -bullet.Speed;
		}
	}
}