using Combat.Prefab;
using Enum;
using Player;
using UnityEngine;
using static Enum.HealthState;

namespace Health
{
	public class HealthController: MonoBehaviour // todo: Hacer HUD de la vida, y marcador de vida de los enemigos
	{
		[SerializeField] private float maxHealth;

		public float Health { get; set; }
		public HealthState State { get; private set; }

		private void Start()
		{
			State = Alive;
			Health = maxHealth;

			if (gameObject.TryGetComponent(out PlayerController player)) return;
		}

		private void Update()
		{
			if (State == Alive) State = Health > 0 ? Alive : Dead;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Projectile"))
			{
				ProjectileController projectile = other.GetComponent<ProjectileController>();

				if (projectile.CanSurpassEnemies && gameObject.CompareTag("Enemy")) return;

				Health -= projectile.Damage;
				Destroy(projectile.gameObject);
			}

			if (other.CompareTag("Attack"))
			{
				AttackController attack = other.GetComponent<AttackController>();

				if (attack.Parent == gameObject) return;

				Health -= attack.Damage;
				Destroy(other.gameObject);
			}
		}
	}
}