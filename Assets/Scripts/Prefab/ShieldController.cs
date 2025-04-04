using Health;
using UnityEngine;
using static Health.HealthState;

namespace Prefab
{
	[RequireComponent(typeof(HealthController), typeof(Collider2D), typeof(Rigidbody2D))]
	public class ShieldController: PrefabManager
	{
		public HealthController HealthController { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			HealthController = GetComponent<HealthController>();
		}

		protected override void Start()
		{
			transform.position = Parent.transform.position + new Vector3(Parent.transform.rotation.y == 0 ? 1.5f : -1.5f, 0, 0);
			base.Start();
		}

		void Update()
		{
			if (HealthController.State == Dead) Destroy(gameObject);
		}
	}
}