using Combat.Prefab;
using Enum;
using Player;
using UnityEngine;
using static Enum.HealthState;

namespace Health
{
	public class HealthController: MonoBehaviour // todo: Hacer marcador de vida de los enemigos
	{
		[SerializeField] private float maxHealth;

		private AudioSource _audio;

		public float MaxHealth => maxHealth;
		public float Health { get; set; }
		public HealthState State { get; private set; }

		private void Start()
		{
			_audio = gameObject.AddComponent<AudioSource>();
			_audio.clip = Resources.Load<AudioClip>("Sounds/HitSound");
			
			State = Alive;
			Health = maxHealth;

			if (gameObject.TryGetComponent(out PlayerController player)) return;
		}

		private void Update()
		{
			if (State == Alive) State = Health > 0 ? Alive : Dead;
			if (State == Dead) gameObject.layer = LayerMask.NameToLayer("Background");
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
			
			_audio.Play();
		}
	}
}