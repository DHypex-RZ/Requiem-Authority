using Character;
using Health;
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
			base.Start();
			transform.position = Parent.transform.position + new Vector3(Parent.transform.rotation.y == 0 ? 1.5f : -1.5f, 0, 0);
		}

		void Update()
		{
			if (HealthController.State == Dead) _ = DisablePrefab();
		}

		public void SetParameters(CharacterManager parent, float health, float duration)
		{
			base.SetParameters(parent, duration);
			HealthController.Health = HealthController.CurrentHealth = health;
		}
	}
}