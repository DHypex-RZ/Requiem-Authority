using Character;
using Health;
using Prefab.Physical;
using UnityEngine;
using static Health.State;

namespace Prefab.Shield
{
	[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
	public class ShieldController: PrefabManager
	{
		public HealthController HealthController { get; } = new();

		protected override void Start()
		{
			transform.position = Parent.transform.position + new Vector3(Parent.transform.rotation.y == 0 ? 1.5f : -1.5f, 0, 0);
			base.Start();
		}

		void Update()
		{
			if (HealthController.State == Dead) Destroy(gameObject);
		}

		public void SetParameters(CharacterManager parent, float health, float duration)
		{
			base.SetParameters(parent, duration);
			HealthController.health = HealthController.CurrentHealth = health;
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.TryGetComponent(out NormalAttack attack))
				if (attack.Parent == Parent)
					Destroy(gameObject);
		}
	}
}